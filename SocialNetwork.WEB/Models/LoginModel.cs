﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class LoginModel
    {
        [EmailAddress(ErrorMessage = "Введите действительный адрес электронной почты")]
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        [Display(Name = "Email")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}