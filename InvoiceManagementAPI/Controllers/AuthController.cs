using Microsoft.AspNetCore.Mvc;
using InvoiceManagementAPI.Services;
using InvoiceManagementAPI.DTOs.Auth;
using System.Threading.Tasks;

namespace InvoiceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // POST api/Auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto dto)
        {
            await _authService.RegisterAsync(dto);
            return Ok(new { Message = "User registered successfully" });
        }

        // POST api/Auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto dto)
        {
            try
            {
                string token = await _authService.LoginAsync(dto.Email, dto.Password);
                return Ok(new { Token = token });
            }
            catch (System.Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}



