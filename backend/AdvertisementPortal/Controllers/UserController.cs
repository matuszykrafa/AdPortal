using AdvertisementPortal.Common.Auth;
using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace AdvertisementPortal.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Login([FromBody] LoginModel user)
        {
            if (user is null) 
                return BadRequest("Invalid client request");

            var isAuthenticated = _userService.TryAuthenticateUser(user, out var token);

            if (!isAuthenticated) 
                return Unauthorized();

            var authResponse = new AuthenticatedResponse() { Token = token };
            return Ok(authResponse);
        }

        [HttpPost, AllowAnonymous]
        public IActionResult Register([FromBody] RegisterModel user)
        {
            try
            {
                if (user is null)
                    return BadRequest("Invalid client request");

                var res = _userService.RegisterUser(user);
                return Ok(res);
            }
            catch (DbUpdateException)
            {
                return Ok(false);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }

        [HttpGet]
        public IActionResult GetUserData()
        {
            var userData = _userService.GetUserData(Request);
            return Ok(userData);

        }
    }
}
