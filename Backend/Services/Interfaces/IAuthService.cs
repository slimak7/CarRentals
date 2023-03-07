using Backend.ResponsesModels;

namespace Backend.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> Register(string email, string password, string firstName, string lastName, string phoneNumber);
    }
}
