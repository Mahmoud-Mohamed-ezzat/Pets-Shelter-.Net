﻿using System.ComponentModel.DataAnnotations;

namespace Animal2.Dto.Account
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
