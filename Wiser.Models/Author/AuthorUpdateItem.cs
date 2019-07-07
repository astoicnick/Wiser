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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
    }
}
