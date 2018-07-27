using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DataTransferObjects
{
    public class DialogMessageDTO
    {
        public int Id { get; set; }

        public int DialogId { get; set; }
        public DialogDTO Dialog { get; set; }

        public int FromUserId { get; set; }
        public UserDTO FromUser { get; set; }

        public string Text { get; set; }

        public bool Status { get; set; }

        public DateTime Date { get; set; }

    }
}
