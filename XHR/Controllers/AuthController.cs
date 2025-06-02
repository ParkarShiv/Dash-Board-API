using Microsoft.AspNetCore.Mvc;
using XHR.Models;  
using XHR.Services;

namespace XHR.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var result = await _authService.RegisterAsync(model.Username, model.Password);
                return Ok(result); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);  
            }
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data.");

            try
            {
                var token = await _authService.LoginAsync(model.Username, model.Password);
                return Ok(new { Token = token });  
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized("Invalid credentials.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
