using Contracts;
using DomainEntities;
using Dto;
using Implementations.CustomExceptions;
using RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementations
{
    public class DeliverService : IDeliverService
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IProductsRepository _productsRepository; 

        public DeliverService(ICustomersRepository customersRepo, IOrdersRepository ordersRepo, IProductsRepository productsRepository)
        {

            _customersRepository = customersRepo;
            _ordersRepository = ordersRepo;
            _productsRepository = productsRepository;
        }

        public string CreateOrder(DeliverDto deliveryDto) 
        {
            ValidateInformation(deliveryDto); // VALIDAMOS INFO 
        
            List<ProductDto> productsDto = FindProducts(deliveryDto);    // RECOGEMOS PRODUCTOS COMO DOMAIN Y PARSEAMOS A DTO      

            CustomerDto customerDto = _customersRepository.FoundUserById(deliveryDto.Customer.Id); // RECOGEMOS EL CUSTOMER COMO DOMAIN Y PARSEAMOS A DTO

            if(customerDto == null)
            {
                throw new NotAllowUserId($"Not found user with ID {deliveryDto.Customer.Id}");
            }

            deliveryDto.Customer = customerDto;    

            InsertProductsIntoDeliver(deliveryDto, productsDto, customerDto);

            customerDto.OrdersId.Add(deliveryDto);

            Deliver deliver = TransformDeliverDtoToEntity(deliveryDto);

            UpdateUserInformation(customerDto, deliver.Id);

            
        }

        private void ValidateInformation(DeliverDto deliveryDto)
        {
            if (deliveryDto.ValidateQuantity() == false)
            {
                throw new NotAllowQuantityProductException("The quantity of products must to be greater than 0");
            }

            if (deliveryDto.Customer.ValidateCustomerId() == false)
            {
                throw new NotAllowUserId($"The ID's must to be natural ones.");
            }
        }

        private List<ProductDto> FindProducts(DeliverDto deliveryDto)
        {
            List<int> productsId = deliveryDto.ProductsQuantity.Keys.Select(e => e.Id).ToList(); // TODO aqui iria mejor un foreach con llamadas a la api o mejor una unica con la lista? 

            List<ProductDto> productsDto = new List<ProductDto>();

            foreach (int productId in productsId)
            {
                Product productFromApi = _productsRepository.GetProduct(productId);

                if(productFromApi == null) 
                { 
                    throw new NotFoundProductsExcepion("The introduced products didn't found");
                
                }

                productsDto.Add(TransformProductEntityToDto(productFromApi));

            }          

            return productsDto;
        }
        private void InsertProductsIntoDeliver(DeliverDto deliveryDto, List<ProductDto> productsDto, CustomerDto customerDto)
        {
            decimal totalPrice = 0; 
            foreach (ProductDto productDto in productsDto)
            {
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Price = _currencyRepository.TransformCoin(productDto.Price, customerDto.CountryShortName.ShortCoinName);
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Category = productDto.Category;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Title = productDto.Title;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Description = productDto.Description;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Image = productDto.Image;

                totalPrice += deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == productDto.Id).Price;
            }

            deliveryDto.TotalPriece = totalPrice + customerDto.CountryShortName.DeliverTaxes;

            deliveryDto.DeliverDay = DateTime.Now.AddDays(3).Date;
        }

        private Deliver TransformDeliverDtoToEntity(DeliverDto deliverDto)
        {
            int lastId = _ordersRepository.FindLastId();
            Deliver deliver = new Deliver
            {
                Id = lastId + 1,
                CustomerId = deliverDto.Customer.Id,
                DeliverDay = deliverDto.DeliverDay,
                TotalPriece = deliverDto.TotalPriece,
               
           
            };

            foreach(ProductDto product in deliverDto.ProductsQuantity.Keys)
            {
                var productQuantity = deliverDto.ProductsQuantity.FirstOrDefault(e => e.Key.Id == product.Id);

                deliver.ProductsIdQuantity.Add(productQuantity.Key.Id ,productQuantity.Value);
            }

            return deliver; 
        }

        private void UpdateUserInformation(CustomerDto customerDto, int deliverId)
        {
            Customer customer = new Customer
            {
                Id = customerDto.Id,
                CountryShortName = customerDto.CountryShortName.ShortName,
                CustomerName = customerDto.CustomerName,
                CustomerSurname = customerDto.CustomerSurname
            };

            customer.OrdersId.Add(deliverId);

            _customersRepository.UpdateUser(customer);


        }

        private ProductDto TransformProductEntityToDto(Product productFromApi)
        {
            return new ProductDto
            {
                Id = productFromApi.Id,
                Category = productFromApi.Category,
                Description = productFromApi.Description,
                Price = productFromApi.Price,
                Title = productFromApi.Title,
                Image = productFromApi.Image
            };
        }

        private CustomerDto TransformCustomerEntityToDto(Customer customer)
        {
            CustomerDto customerDto = new CustomerDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                CustomerSurname = customer.CustomerSurname,
                

            };

            foreach(int deliverId in customer.OrdersId)
            {
                Deliver deliver = _ordersRepository.FindDeliverById(deliverId); // RECOJO EL DELIVER DOMAIN 

                foreach(int key in deliver.ProductsIdQuantity.Keys)
                {
                    Product product = _productsRepository.GetProduct(key); // RECOJO EL PRODUCTO POR CADA ID QUE TIENE ALMACENADO EN LA CANTIDAD DE PRODUCTOS 

                    ProductDto productDto = TransformProductEntityToDto(product);

                    DeliverDto deliverDto = new DeliverDto
                    {
                        
                    }
                }
                
                customerDto.OrdersId.Add()
            }
        }


    }
}
