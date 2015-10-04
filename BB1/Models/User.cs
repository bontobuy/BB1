﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BB1.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required, Display(Name = "Email"), EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}