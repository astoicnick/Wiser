using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Data;

namespace Wiser.Models.Wisdom
{
    public class WisdomContributionItem
    {
        [Required]
        [Display(Name = "Created At")]

        public DateTime CreatedAt { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }
        [Required]
        public int WisdomId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public Genre WisdomGenre { get; set; }
        [UIHint("Upvoted")]
        public bool IsUpvoted { get; set; }
    }
}
