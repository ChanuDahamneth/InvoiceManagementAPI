using InvoiceManagementAPI.DTOs.Auth;
using InvoiceManagementAPI.Helpers;
using InvoiceManagementAPI.Models;
using InvoiceManagementAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceManagementAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserRepository _repo;
        private readonly JwtTokenGenerator _jwt;

        public AuthController(UserRepository repo, JwtTokenGenerator jwt)
        {
            _repo = repo;
            _jwt = jwt;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = PasswordHasher.Hash(dto.Password),
                Role = "User"
            };

            await _repo.CreateAsync(user);
            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null) return Unauthorized();

            if (user.PasswordHash != PasswordHasher.Hash(dto.Password))
                return Unauthorized();

            var token = _jwt.GenerateToken(user.Id, user.Role);
            return Ok(new { token });
        }
    }
}

