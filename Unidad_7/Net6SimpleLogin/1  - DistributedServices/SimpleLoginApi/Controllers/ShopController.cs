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

        private readonly ILogger<SimpleLoginController> _logger;

        public ShopController(ITrackOrderService trackOrderService, ILoginUserService loginUserService, ILogger<SimpleLoginController> logger)
        {

            _trackOrderService = trackOrderService;

            _loginUserService = loginUserService;
            
            _logger = logger;   
        }
        /// <summary>
        ///  This method adds product to your deliver.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddProduct")]
        public (IActionResult, string) AddProduct(ProductModel request)
        {
            try
            {
                if (!ValidateLogin())
                {
                    _logger.LogError("Try action without had been login.");
                    return (BadRequest("Must to login first."), "Not login");
                }

                string userName = GetActualUser();

                string response = _trackOrderService.AddProduct(userName, request.ProductName);

                if(response == null)
                {
                    _logger.LogError("Product not found.");

                    return (BadRequest("NotFound"), "Error404" );
                }

                return (Ok("Add product"), response);

                //TODO 2806 revisar como devolver la tupla 
            }
            catch (NotEnoughtStockException ex)
            {
                _logger.LogError("Not enought stock.");
                return (BadRequest("Not enought stock"), ex.Message);
            }
            catch (DataIntroducedErrorException ex)
            {
                _logger.LogError("Product not found.");
                return (BadRequest("Not found product"), ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError("Critical error.");
                return (BadRequest(), "test");
                // REVISAR ERROR CODES PARA DEVOLVER ERROR 
            }
        }
        /// <summary>
        /// This method shows you the orders wich it's on your profile, with the time you will recive. 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTrackProductsValidating")]
        public (IActionResult, string) GetProducts()
        {
            if (!ValidateLogin())
            {
                _logger.LogError("Try action without had been login.");
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
                _logger.LogWarning("Not found orders.");
                return (BadRequest("Not found orders"), ex.Message);
            }

            //TODO 2806 revisar como devolver la tupla 

        }

        /// <summary>
        /// Thats not make nothing, just for educational propose.
        /// </summary>
        /// <returns></returns>
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
                _logger.LogError($"Invalid token: {ex.Message}");
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

