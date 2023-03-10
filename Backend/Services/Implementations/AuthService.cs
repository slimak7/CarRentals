using Backend.DBLogic.DBModels;
using Backend.DBLogic.Repos.Users;
using Backend.ResponsesModels;
using Backend.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Backend.Services.Implementations
{
    public class AuthService : IAuthService
    {

        private IUsersRepo _usersRepo;
        private IConfiguration _config;

        public AuthService(IUsersRepo usersRepo, IConfiguration config)
        {
            _usersRepo = usersRepo;
            _config = config;
        }

        public async Task<AuthResponse> Login(string email, string password)
        {
            var user = await _usersRepo.GetByCondition(x => x.Email == email);

            if (user == null)
            {
                throw new Exception("User with this email not found");
            }
            else
            {
                if (user.Password == password)
                {
                    string token = GenerateToken(user);

                    return new AuthResponse(null, true, user.UserID.ToString(), token, user.UserType.TypeName, user.Email);
                }
                else
                {
                    throw new Exception("Wrong password");
                }
            }
        }

        public async Task<AuthResponse> Register(string email, string password, string firstName, string lastName, string phoneNumber)
        {
            var user = await _usersRepo.GetByCondition(x => x.Email == email);

            if (user == null)
            {
                var newUser = await _usersRepo.Add(new User()
                {
                    Email = email,
                    Password = password,
                    FirstName = firstName,
                    LastName = lastName,
                    PhoneNumber = phoneNumber
                });

                string token = GenerateToken(newUser);

                return new AuthResponse(null, true, newUser.UserID.ToString(), token, newUser.UserType.TypeName, user.Email);
            }
            else
            {
                throw new Exception("This email is already used");
            }
        }

        private string GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _config["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var role = user.UserType;

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role.TypeName),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials);

            var writenToken = new JwtSecurityTokenHandler().WriteToken(token);

            return writenToken;
        }


    }
}
