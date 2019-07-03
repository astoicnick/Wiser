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
        public int AuthorId { get; set; }
        [Required]
        public int? Virtue { get; set; }
        [Required(AllowEmptyStrings =false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string LastName { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public List<WisdomAttributionItem> Attributions { get; set; }
    }
}
