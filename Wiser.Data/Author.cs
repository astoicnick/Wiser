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
        public string LastName { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Virtue { get; set; } = 0;
        public int WisdomCount { get; set; }
    }
}