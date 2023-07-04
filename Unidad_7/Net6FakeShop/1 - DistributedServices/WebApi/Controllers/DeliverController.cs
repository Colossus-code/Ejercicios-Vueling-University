using Contracts;
using Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliverController : ControllerBase
    {
        private readonly IDeliverService _deliverService;

        public DeliverController(IDeliverService deliverService)
        {

            _deliverService = deliverService;

        }

        [HttpPost]
        [Route("CreateDeliver")]
        public IActionResult CreateDeliver([Required]DeliverModel deliverModel)
        {
            DeliverDto deliverDto = new DeliverDto
            {
                Customer = new CustomerDto
                {
                    Id = deliverModel.UserId
                },
                ProductsQuantity = new Dictionary<ProductDto, int>()


            };

            foreach(int key in deliverModel.ProductQuantity.Keys) 
            {
                deliverDto.ProductsQuantity.Add(new ProductDto
                {
                    Id = key
                }, 
                value: deliverModel.ProductQuantity[key]

                );
            }

            string response = "Oken";
            return Ok(response); 
        }
    }
}
