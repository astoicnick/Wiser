using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Data;
using Wiser.Models.Author;

namespace Wiser.Models.Wisdom
{
    public class WisdomCreateItem
    {
        public AuthorCreateItem Author { get; set; }
        //public int? AuthorId { get; set; }
        //[Display(Name ="First Name")]
        //public string AuthorFirstName { get; set; }
        //[Display(Name = "Last Name")]
        //public string AuthorLastName { get; set; }
        //[Required(AllowEmptyStrings =false,ErrorMessage ="Please enter valid content for this wisdom.")]
        //[MinLength(10,ErrorMessage ="Must be at least 10 characters")]
        public string Content { get; set; }
        [Required]
        [MinLength(10, ErrorMessage = "Must be at least 10 characters")]
        public string Source { get; set; }

        [Required]
        [Display(Name ="Genre")]
        public Genre WisdomGenre { get; set; }

    }
}
