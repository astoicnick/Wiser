using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Author
{
    public class AuthorUpdateItem
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]

        public string LastName { get; set; }
        [Display(Name = "Name")]
        [Required]
        public string FullName { get; set; }
        [Required]
        [Display(Name = "Created At")]
        public DateTime CreatedAt { get; set; }
    }
}
