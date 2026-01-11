using Microsoft.AspNetCore.Mvc;
using InvoiceManagementAPI.Services;
using InvoiceManagementAPI.DTOs.Auth;
using InvoiceManagementAPI.Helpers;

namespace InvoiceManagementAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly JwtTokenGenerator _jwtTokenGenerator;

    public AuthController(
        AuthService authService,
        JwtTokenGenerator jwtTokenGenerator)
    {
        _authService = authService;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    [HttpPost("register")]  // ✅ THIS ROUTE MUST EXIST
    public IActionResult Register(RegisterRequestDto dto)
    {
        // Call your service to register user
        _authService.Register(); // Minimal logic for now

        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public IActionResult Login(LoginRequestDto dto)
    {
        int userId = 1;           // Replace with DB lookup
        string email = dto.Email;
        string role = "User";     // Replace with DB role

        string token = _jwtTokenGenerator.GenerateToken(userId, email, role);

        return Ok(new { token });
    }
}
