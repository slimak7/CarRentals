using System.ComponentModel.DataAnnotations;

namespace Backend.RequestsModels
{
    public class LogInRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
