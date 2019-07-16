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
        public string Role { get; set; }
        public string Name { get; set; }
        public int? Virtue { get; set; }
        public int? VirtueToGiveToday { get; set; }
        [Display(Name = "Created At")]

        public DateTime CreatedAt { get; set; }
        public List<WisdomScrollItem> Contributions { get; set; }
        public List<WisdomScrollItem> Favorites { get; set; }
    }
}
