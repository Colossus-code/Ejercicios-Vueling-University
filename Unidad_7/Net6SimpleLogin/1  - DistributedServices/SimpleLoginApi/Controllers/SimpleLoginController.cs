using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SimpleLoginApi.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SimpleLoginApi.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    public class SimpleLoginController : ControllerBase
    {
        private readonly ILoginUserService _loginUserService;
        private readonly IRegistUserService _registUserService;
        private readonly ITrackOrderService _trackOrderService;

        private readonly IConfiguration _configuration;
        public SimpleLoginController(IRegistUserService registUserService, ITrackOrderService trackOrderService, ILoginUserService loginUserService,
            IConfiguration config)
        {
            _registUserService = registUserService;

            _trackOrderService = trackOrderService;

            _loginUserService = loginUserService;

            _configuration = config;
        }

        [HttpPost]
        [Route("RegistUser")]
        public async Task<IActionResult> RegistUser(UserModel request)
        {
           var userDto = TransformModel(request);

            try
            {
                await _registUserService.GenerateUser(userDto);
                
                CreateToken(userDto);

                return Ok(userDto.PasswordSalt);
            }
            catch(DbUpdateException ex)
            {
                //todo sgarciam meter logger aqui 

                return BadRequest("User allready registrated with this username.");
            }            
            catch
            {
                return BadRequest();
            }

        }        
        
        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(UserModel request)
        {
            var userDto = new UserDto
            {
                Username = request.UserName,
            };

            try
            {
                if (_loginUserService.LoggingUser(userDto, request.UserPass))
                {
                    CreateToken(userDto);

                    return Ok(userDto.PasswordSalt);
                }

                return BadRequest("User or password wrong");
                
            }
            catch(DataIntroducedErrorException ex)
            {
                return BadRequest($"{ex.Message}"); 
            }
            catch
            {
                return BadRequest();
            }

        }

        private UserDto TransformModel(UserModel request)
        {

            UserDto userDto = new UserDto
            {
                Username = request.UserName,
            };

            CreatePasswordHash(request.UserPass, out byte[] passwordHash, out byte[] passwordSalt);

            userDto.PasswordHash = passwordHash;
            userDto.PasswordSalt = passwordSalt;

            return userDto;
        }
        private void CreatePasswordHash (string password, out byte[] passwordHash, out byte[] passwordSalt) 
        { 
        
            using( var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));   

            }
        
        }
        private string CreateToken(UserDto userDto)
        {
            List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.Name, userDto.Username + userDto.PasswordHash),


            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var securityToken = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credential

                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return jwt;
        }

    }
}
