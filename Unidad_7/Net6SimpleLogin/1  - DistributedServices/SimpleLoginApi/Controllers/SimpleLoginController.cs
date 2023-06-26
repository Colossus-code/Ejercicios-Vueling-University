using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimpleLoginApi.Model;

namespace SimpleLoginApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SimpleLoginController : ControllerBase
    {
        private readonly ILoginUserService _loginUserService;
        private readonly IRegistUserService _registUserService;
        private readonly ITrackOrderService _trackOrderService;
        public SimpleLoginController(IRegistUserService registUserService, ITrackOrderService trackOrderService, ILoginUserService loginUserService)
        {
            _registUserService = registUserService;

            _trackOrderService = trackOrderService;

            _loginUserService = loginUserService;

        }

        [HttpPost]
        [Route("RegistUser")]
        public IActionResult RegistUser(UserModel userModel)
        {
            return Ok("Oka");
        }
    }
}
