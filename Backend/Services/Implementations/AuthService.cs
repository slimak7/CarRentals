using Backend.ResponsesModels;
using Backend.Services.Interfaces;

namespace Backend.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<AuthResponse> Register(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            throw new NotImplementedException();
        }
    }
}
