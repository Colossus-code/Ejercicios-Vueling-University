using DomainEntities;

namespace Dto
{
    public class DeliverDto
    {
        public List<ProductDto> Products { get; set; }

        public CustomerDto Customer { get; set; }

        public decimal TotalPriece { get; set; }
    }
}