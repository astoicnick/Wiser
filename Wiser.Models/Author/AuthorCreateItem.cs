using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Author
{
    public class AuthorCreateItem
    {
        public int? AuthorId { get; set; }
        [Display(Name ="Last Name")]
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid first name")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
    }
}
