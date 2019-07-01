using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Data
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string FullName => $"{FirstName} {LastName}";
        [Required]
        public DateTime CreatedAt => DateTime.UtcNow;
        [Required]
        public int? Virtue { get; set; }


    }
}