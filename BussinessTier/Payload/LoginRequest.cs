﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessTier.Payload
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "Username is missing")]
        [MaxLength(50, ErrorMessage = "Username's max length is 50 characters")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Password is missing")]
        [MaxLength(64, ErrorMessage = "Password's max length is 64 characters")]
        public string Password { get; set; }
    }
}
