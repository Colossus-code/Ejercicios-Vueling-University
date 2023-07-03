using Contracts;
using Dto;
using Implementations.CustomExceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("/Usuarios/")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUserService _userService; 
        public CustomerController(IUserService userService)
        {

            _userService = userService;

        }

        /// <summary>
        /// Create a quantity of random users. 
        /// </summary>
        /// <returns> Ok if everything was ok, error code if something was wrong.</returns>
        [HttpPost]
        [Route("CreateUsers")]
        public IActionResult GenerateUsers(List<UserModel> models)
        {
            List<CustomerDto> customersDto = new List<CustomerDto>();

            foreach (UserModel userModel in models)
            {
                customersDto.Add(new CustomerDto
                {
                    Id = userModel.Id,
                    CountryShortName = new CountryDto
                    {
                        ShortName = userModel.ShortNationality
                    }
                });

                
            };

            try
            {
                string response = _userService.AgregateUsers(customersDto);

                return Ok(response);
            }catch(NotAllowLocationException ex)
            {
                return BadRequest(ex.Message);
            }            
            catch(NotAllowUserId ex)
            {
                return BadRequest(ex.Message);
            }
            
        }
    }
}
