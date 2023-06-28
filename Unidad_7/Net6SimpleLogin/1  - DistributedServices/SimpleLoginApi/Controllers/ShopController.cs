using Contracts;
using Contracts.CustomExceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using SimpleLoginApi.Model;
using System.Security.Claims;

namespace SimpleLoginApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly ILoginUserService _loginUserService;
        private readonly ITrackOrderService _trackOrderService;

        public ShopController(ITrackOrderService trackOrderService, ILoginUserService loginUserService)
        {

            _trackOrderService = trackOrderService;

            _loginUserService = loginUserService;
        }

        [HttpPost]
        [Route("AddProduct")]
        public (IActionResult, string) AddProduct(ProductModel request)
        {
            try
            {
                if (!ValidateLogin())
                {
                    return (BadRequest("Must to login first."), "Not login");
                }

                string userName = GetActualUser();

                string response = _trackOrderService.AddProduct(userName, request.ProductName);

                return (Ok("Add product"), response);

                //TODO 2806 revisar como devolver la tupla 
            }
            catch (NotEnoughtStockException ex)
            {
                return (BadRequest("Not enought stock"), ex.Message);
            }
            catch (DataIntroducedErrorException ex)
            {
                return (BadRequest("Not found product"), ex.Message);
            }
            catch (Exception ex)
            {
                return (BadRequest(), "test");
                // REVISAR ERROR CODES PARA DEVOLVER ERROR 
            }
        }

        [HttpGet]
        [Route("GetTrackProductsValidating")]
        public (IActionResult, string) GetProducts()
        {
            if (!ValidateLogin())
            {
                return (BadRequest("Must to login first."), "Not login");
            }

            string userName = GetActualUser();

            try
            {

                string response = _trackOrderService.GetTrack(userName);

                return (Ok("Found products"), response);
            }
            catch (NotOrdersException ex)
            {
                return (BadRequest("Not found orders"), ex.Message);
            }

            //TODO 2806 revisar como devolver la tupla 

        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [Route("GetTrackProductsAuthorize")]
        public IActionResult GetProductsAuthorized()
        {

            return Ok("It worked!");

            // Metodo de prueba, simplemente vale para ver otra manera de verificar que el Token funciona desde Swagger. 
        }
        private bool ValidateLogin()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            try
            {
                return _loginUserService.ValidateToken(_bearer_token);
            }
            catch (ArgumentNullException ex)
            {
                //REGISTRAR EN EL LOGGER 
                return false;
            }
        }

        private string GetActualUser()
        {
            string userName = string.Empty;

            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                userName = claims.First().Value;

            }

            int separator = userName.IndexOf("-");

            return userName.Remove(separator);
            //TODO 2806 preguntar al profe si esto iría en la capa de service es logica de negocio? yo diria no  
        }
    }
}

