﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Models
{
    public class UserModel
    {
        public string? Id { get; set; }

        [Required]
        public string UserName { get; set; }


        [Required]
        [RegularExpression("^[FM]$", ErrorMessage = "Gender must be 'F' or 'M'.")]
        public char Gender { get; set; }

        [Required]
        // [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$\r\n", ErrorMessage = "Email does not follow the pattern")]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$")]
        public string Password { get; set; }

        public IFormFile? Image { get; set; }
    }
}
