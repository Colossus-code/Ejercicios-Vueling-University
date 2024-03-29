﻿using Contracts;
using Contracts.CustomExceptions;
using Contracts.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
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
        private readonly ILogger<SimpleLoginController> _logger;

        private readonly IConfiguration _configuration;
        public SimpleLoginController(IRegistUserService registUserService, ILoginUserService loginUserService, IConfiguration config, ILogger<SimpleLoginController> logger)
        {
            _registUserService = registUserService;

            _loginUserService = loginUserService;

            _configuration = config;
            
            _logger = logger;
        }
        /// <summary>
        /// This method makes you regist into the application and gives the Token. 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("RegistUser")]
        public async Task<IActionResult> RegistUser(UserModel request)
        {
           var userDto = TransformModel(request, true);

            try
            {
                await _registUserService.GenerateUser(userDto);
                
                var token = CreateToken(userDto);

                return Ok(token);
            }
            catch(DbUpdateException ex)
            {

                _logger.LogError("Allready user with this username");
                return BadRequest("User allready registrated with this username.");
            }            
            catch
            {
                return BadRequest();
            }

        }        
        /// <summary>
        /// This method regist the token and gives you.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("LoginUser")]
        public IActionResult LoginUser(UserModel request)
        {
            var userDto = TransformModel(request,false);

            try
            {
                if (_loginUserService.LoggingUser(userDto))
                {
                    var token = CreateToken(userDto);

                    return Ok(token);
                }

                _logger.LogError("User or password wrong.");
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
        
        private UserDto TransformModel(UserModel request, bool encypt)
        {

            UserDto userDto = new UserDto
            {
                Username = request.UserName,
                Password = request.UserPass
            };

            if(encypt == true)
            {
                CreatePasswordHash(request.UserPass, out byte[] passwordHash, out byte[] passwordSalt);

                userDto.PasswordHash = passwordHash;
                userDto.PasswordSalt = passwordSalt;

                return userDto;
            }
            else
            {
                return userDto;
            }
            
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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Key").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            
            List<Claim> claims = new List<Claim>
            {

                new Claim(ClaimTypes.NameIdentifier, $"{userDto.Username}-{userDto.Password}")


            };


            var securityToken = new JwtSecurityToken
                (
                    issuer: _configuration.GetSection("Jwt:Issuer").Value,
                    audience: _configuration.GetSection("Jwt:Audience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: credentials
                    
                    
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return jwt;
        }

    }
}
