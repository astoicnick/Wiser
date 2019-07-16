using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Wisdom
{
    public class WisdomFavoriteItem
    {
        [Required]
        public int FavoriteId { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "Author Name")]

        public string AuthorName { get; set; }
        [Required]
        public int WisdomId { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
    }
}
