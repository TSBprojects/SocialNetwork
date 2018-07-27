using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class UserInDialog
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int DialogId { get; set; }
        public virtual Dialog Dialog { get; set; }

        [Required]
        public int MemberId { get; set; }
        public virtual User Member { get; set; }
    }
}
