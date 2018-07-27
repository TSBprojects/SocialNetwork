using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.DAL.Entities
{
    public class Image
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string FilePath { get; set; }
    }
}
