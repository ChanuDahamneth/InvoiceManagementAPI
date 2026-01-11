using InvoiceManagementAPI.Helpers;
using InvoiceManagementAPI.Repositories;
using InvoiceManagementAPI.DTOs.Auth;
using InvoiceManagementAPI.Models;
using System.Threading.Tasks;
using BCrypt.Net;

namespace InvoiceManagementAPI.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(UserRepository userRepository, JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // Login
        public async Task<string> LoginAsync(string email, string password)
        {
            // 1️⃣ Check if it's the hardcoded admin
            if (email.ToLower() == "admin@gmail.com" && password == "Admin@123")
            {
                // Create JWT token with Admin role
                return _jwtTokenGenerator.GenerateToken(0, email, "Admin");
            }

            // 2️⃣ Otherwise, normal user login from DB
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null)
                throw new Exception("User not found");

            // Verify password
            bool valid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
            if (!valid)
                throw new Exception("Invalid password");

            return _jwtTokenGenerator.GenerateToken(user.Id, user.Email, user.Role);
        }

        // Register new normal user
        public async Task RegisterAsync(RegisterRequestDto dto)
        {
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Role = "User"
            };

            await _userRepository.CreateAsync(user);
        }
    }
}

