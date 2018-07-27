using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class Dialog
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }

        [Required]
        public int ProfileImageId { get; set; }
        public virtual Image ProfileImage { get; set; }

        public virtual List<UserInDialog> UsersInDialog { get; set; }

        public virtual List<DialogMessage> DialogMessages { get; set; }
    }
}
