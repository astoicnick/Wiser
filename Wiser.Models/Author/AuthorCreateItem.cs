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
        public string LastName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter a valid first name")]
        public string FirstName { get; set; }
    }
}
