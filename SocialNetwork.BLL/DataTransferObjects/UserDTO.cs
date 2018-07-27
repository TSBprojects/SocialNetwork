using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DataTransferObjects
{
    public class UserDTO
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public int HashPassword { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public bool Sex { get; set; }

        public bool Online { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public int ProfileImageId { get; set; }
        public ImageDTO ProfileImage { get; set; }

        public int TempDialogId { get; set; }
        public DialogDTO TempDialog { get; set; }
    }
}
