using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Author
{
    public class AuthorScrollItem
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "Name")]

        public string AuthorName { get; set; }
        [Required]
        //How many wisdom posts that author has been attributed to (count of attributions)
        public int WisdomCount { get; set; }
    }
}
