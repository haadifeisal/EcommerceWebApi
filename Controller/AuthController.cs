using EcommerceWebApi.DataTransferObject;
using EcommerceWebApi.Domain.ServiceManager.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EcommerceWebApi.Controller
{
    [ApiController]
    [Route("v1/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly IAuthServiceManager _authServiceManager;
        private readonly AppSettings _appSettings;

        public AuthController(IAuthServiceManager authServiceManager,
            IOptions<AppSettings> appSettings)
        {
            _authServiceManager = authServiceManager;
            _appSettings = appSettings.Value;
        }

        /// <summary>
        /// User Login
        /// </summary>
        /// <param name="userRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [Route("login")]
        public IActionResult Login(UserRequestDto userRequestDto)
        {
            var user = _authServiceManager.Login(userRequestDto.Username, userRequestDto.Password);
            var admin = _authServiceManager.AdminLogin(userRequestDto.Username, userRequestDto.Password);

            var generatedToken = _authServiceManager.GenerateToken(user, admin);

            if (string.IsNullOrWhiteSpace(generatedToken))
            {
                return Unauthorized();
            }

            return Ok(new
            {
                token = generatedToken
            });

        }

        /// <summary>
        /// Register a customer.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="customerRequestDto"></param>
        /// <returns></returns>
        [HttpPost("customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult RegisterCustomer([FromBody] CustomerRequestDto customerRequestDto)
        {
            var userRegistered = _authServiceManager.RegisterCustomer(customerRequestDto);

            if (!userRegistered)
            {
                return UnprocessableEntity();
            }

            return Ok();
        }

        /// <summary>
        /// Register a admin.
        /// </summary>
        /// <param name="userRequestDto"></param>
        /// <returns></returns>
        [Authorize(Roles = "Admin")]
        [HttpPost("admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public IActionResult RegisterAdmin([FromBody] UserRequestDto userRequestDto)
        {
            var adminRegistered = _authServiceManager.AddAdmin(userRequestDto);

            if (!adminRegistered)
            {
                return UnprocessableEntity();
            }

            return Ok();
        }

    }
}
