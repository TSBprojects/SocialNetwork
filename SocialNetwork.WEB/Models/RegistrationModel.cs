using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class RegistrationModel
    {
        [MinLength(2, ErrorMessage = "Имя должно содержать содержать мимнимум 2 символа")]
        [MaxLength(50, ErrorMessage = "Имя должно содержать содержать максмимум 50 символо")]
        [Required(ErrorMessage = "Введите имя")]
        [Display(Name = "Имя")]
        public string FirstName { get; set; }

        [MinLength(2, ErrorMessage = "Имя должно содержать содержать мимнимум 2 символа")]
        [MaxLength(50, ErrorMessage = "Имя должно содержать содержать максмимум 50 символо")]
        [Required(ErrorMessage = "Введите фамилию")]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Введите действительный адрес электронной почты")]
        [Required(ErrorMessage = "Введите адрес электронной почты")]
        public string Email { get; set; }

        [MinLength(6, ErrorMessage = "Пароль должен содержать содержать мимнимум 6 символа")]
        [MaxLength(20, ErrorMessage = "Пароль должен содержать содержать максмимум 20 символо")]
        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Required(ErrorMessage = "Подтвердите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердите пароль")]
        public string ConfirmPassword { get; set; }
    }
}