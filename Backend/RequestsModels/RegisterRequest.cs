﻿using System.ComponentModel.DataAnnotations;

namespace Backend.RequestsModels
{
    public class RegisterRequest
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Phone]
        [Required]
        public string PhoneNumber { get; set; }
    }
}
