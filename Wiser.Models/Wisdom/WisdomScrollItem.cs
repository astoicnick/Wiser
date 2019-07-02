using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Author;

namespace Wiser.Models.Wisdom
{
    public class WisdomScrollItem
    {
        [Key]
        public int WisdomId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Display(Name = "User")]
        public string UserName => $"{FirstName} {LastName}";
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public AuthorScrollItem Author { get; set; }
        public int? Virtue { get; set; }
    }
}
