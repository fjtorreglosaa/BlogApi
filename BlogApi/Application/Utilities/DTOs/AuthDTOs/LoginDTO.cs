﻿using System.ComponentModel.DataAnnotations;

namespace BlogApi.Application.Utilities.DTOs.AuthDTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
