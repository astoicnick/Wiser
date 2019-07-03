using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Wisdom;

namespace Wiser.Models.User
{
    public class UserDetailItem
    {
        public string UserId { get; set; }
        //Supposed to be either "user" or "admin"
        [Required]
        public string Role { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int? Virtue { get; set; }
        [Required]
        public int? VirtueToGiveToday { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<WisdomScrollItem> Contributions { get; set; }
        public List<WisdomScrollItem> Favorites { get; set; }
    }
}
