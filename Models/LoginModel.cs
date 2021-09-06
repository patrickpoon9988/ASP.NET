﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="Username is missing!")]
        [Display(Name = "Account")]
        public string username { get; set; }

        [Required(ErrorMessage ="Password is missing!")]
        [Display(Name = "Password")]
        public string password { get; set; }

    }
}