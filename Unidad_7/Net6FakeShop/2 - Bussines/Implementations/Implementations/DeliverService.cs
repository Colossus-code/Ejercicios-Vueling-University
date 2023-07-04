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
            ValidateInformation(deliveryDto);
        
            List<ProductDto> productsDto = FindProducts(deliveryDto);         

            CustomerDto customerDto = _customersRepository.FoundUserById(deliveryDto.Customer.Id);

            if(customerDto == null)
            {
                throw new NotAllowUserId($"Not found user with ID {deliveryDto.Customer.Id}");
            }

            deliveryDto.Customer = customerDto;    

            InsertProductsIntoDeliver(deliveryDto, productsDto, customerDto);

            customerDto.OrdersId.Add(deliveryDto);

            Deliver deliver = TransformDtoToEntity(deliveryDto);

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
                productsDto.Add(_productsRepository.GetProducts());

            }

            if (productsDto.Count < 0)
            {

                throw new NotFoundProductsExcepion("The introduced products didn't found");

            }

            return productsDto;
        }
        private void InsertProductsIntoDeliver(DeliverDto deliveryDto, List<ProductDto> products, CustomerDto customerDto)
        {
            decimal totalPrice = 0; 
            foreach (ProductDto product in products)
            {
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Price = _currencyRepository.TransformCoin(product.Price, customerDto.CountryShortName.ShortCoinName);
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Category = product.Category;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Title = product.Title;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Description = product.Description;
                deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Image = product.Image;

                totalPrice += deliveryDto.ProductsQuantity.Keys.FirstOrDefault(e => e.Id == product.Id).Price;
            }

            deliveryDto.TotalPriece = totalPrice + customerDto.CountryShortName.DeliverTaxes;

            deliveryDto.DeliverDay = DateTime.Now.AddDays(3).Date;
        }

        private Deliver TransformDtoToEntity(DeliverDto deliverDto)
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
         
    }
}
