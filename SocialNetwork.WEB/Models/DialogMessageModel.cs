using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork.WEB.Models
{
    public class DialogMessageModel
    {
        [Required(ErrorMessage = "Выберите получателя")]
        public int DialogId { get; set; }

        public int FromUserId { get; set; }

        [Required(ErrorMessage = "Введите сообщение")]
        public string Text { get; set; }
    }
}