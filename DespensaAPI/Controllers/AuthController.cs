// AuthController.cs
using Microsoft.AspNetCore.Mvc;
using DespensaAPI.Models;
using DespensaAPI.Services;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace DespensaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        public AuthController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserModel model)
        {
            var user = await _userService.AuthenticateAsync(model.Username, model.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var token = _jwtService.GenerateJwtToken(user);

            return Ok(new { Token = token });
        }

        [HttpGet("protected")]
        [Authorize]
        public ActionResult Protected()
        {
            return Ok("This is a protected endpoint");
        }
    }
}
