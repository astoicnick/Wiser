using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.User
{
    public class UserScrollItem
    {
        [Required]
        public Guid UserId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
