using IdentityService.Models;
using IdentityService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace IdentityService.Controllers
{
    public class AuthController : ControllerBase
    {
        private IAdminAuthService _adminAuthService;

        public AuthController(IAdminAuthService adminAuthService)
        {
            _adminAuthService = adminAuthService;
        }

        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
            var result = await _adminAuthService.Login(userForLoginDto);
            if (result.Success)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(1));
                Response.Cookies.Append("UserToken", result.Data.Token, cookieOptions);
                return Ok(result.Data.Token);
            }
            return BadRequest("Giriş Başarısız");
        }

        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto adminUser)
        {
            var result = await _adminAuthService.Register(adminUser);
            if (result.Success)
            {
                CookieOptions cookieOptions = new CookieOptions();
                cookieOptions.Expires = new DateTimeOffset(DateTime.Now.AddDays(1));
                Response.Cookies.Append("UserToken", result.Data.Token, cookieOptions);
                return Ok("Kayıt Başarılı");
            }
            return BadRequest("Kayıt Başarısız");
        }
        [AllowAnonymous]
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("logout")]
        public ActionResult LogOut()
        {
            Response.Cookies.Delete("UserToken");

            return Ok("Çıkış Yapıldı");
        }


    }
}