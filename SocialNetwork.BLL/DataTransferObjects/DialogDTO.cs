using SocialNetwork.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.BLL.DataTransferObjects
{
    public class DialogDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ProfileImageId { get; set; }
        public ImageDTO ProfileImage { get; set; }

        public DialogMessageDTO LastMessInDialog { get; set; }
    }
}
