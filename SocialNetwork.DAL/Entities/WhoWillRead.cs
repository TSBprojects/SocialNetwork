using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class WhoWillRead
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int MessageId { get; set; }
        public virtual DialogMessage Message { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
