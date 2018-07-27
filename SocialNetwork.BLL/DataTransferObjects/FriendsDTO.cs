using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DataTransferObjects
{
    public class FriendsDTO
    {
        public int Id { get; set; }

        public int FromUserId { get; set; }
        public UserDTO FromUser { get; set; }

        public int ToUserId { get; set; }
        public UserDTO ToUser { get; set; }

        public int Status { get; set; }
    }
}
