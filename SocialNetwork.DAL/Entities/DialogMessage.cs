using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class DialogMessage
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }

        [Required]
        public int FromUserId { get; set; }
        public virtual User FromUser { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public virtual List<WhoWillRead> UsersWhoWillRead { get; set; }
    }
}
