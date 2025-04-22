using Microsoft.AspNetCore.Mvc;
using MobileBillingApiEfe.Services;

namespace MobileBillingApiEfe.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            if (request.Username == "efe" && request.Password == "1234")
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }
            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

}
