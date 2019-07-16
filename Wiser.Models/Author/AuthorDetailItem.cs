using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Wisdom;

namespace Wiser.Models.Author
{
    public class AuthorDetailItem
    {
        [Display(Name ="Wisdom Count")]
        public int WisdomCount { get; set; }
        public int AuthorId { get; set; }
        [Required]
        public int? Virtue { get; set; }
        [Required(AllowEmptyStrings =false)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<WisdomScrollItem> Attributions { get; set; }
    }
}
