using InvoiceManagementAPI.Helpers;
using InvoiceManagementAPI.Repositories;

namespace InvoiceManagementAPI.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly JwtTokenGenerator _jwtTokenGenerator;

        public AuthService(
            UserRepository userRepository,
            JwtTokenGenerator jwtTokenGenerator)
        {
            _userRepository = userRepository;
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        // This is a MINIMUM login flow (assignment-level)
        public string Login(int userId, string email, string role)
        {
            // Normally you would validate password here
            // Kept minimal due to time constraints

            return _jwtTokenGenerator.GenerateToken(userId, email, role);
        }

        // Placeholder for register logic
        public void Register()
        {
            // Intentionally minimal
            // UserRepository will handle DB logic
        }
    }
}