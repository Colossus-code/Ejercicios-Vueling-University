using DomainEntities;

namespace Dto
{
    public class DeliverDto
    {
        public Dictionary<ProductDto,int> ProductsQuantity { get; set; }

        public CustomerDto Customer { get; set; }

        public decimal TotalPriece { get; set; }

        public DateTime DeliverDay { get; set; }

        public bool ValidateQuantity()
        {
            
            if(ProductsQuantity.Count < 0 || ProductsQuantity.Values.FirstOrDefault(e => e == 0) != 0)
            {
                return false;
            }

            return true;    
        }
    }
}