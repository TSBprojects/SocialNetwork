using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class Friends
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int FromUserId { get; set; }
        public virtual User FromUser { get; set; }

        [Required]
        public int ToUserId { get; set; }
        public virtual User ToUser { get; set; }

        [Required]
        public int Status { get; set; }
    }
}
