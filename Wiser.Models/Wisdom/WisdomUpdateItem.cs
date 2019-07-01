using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Data;

namespace Wiser.Models.Wisdom
{
    public class WisdomUpdateItem
    {
        public int WisdomId { get; set; }
        public Guid UserId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        [Display(Name = "Author")]
        //Put in string for author name so I could link Author's id to  his/her name
        public string AuthorName { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public Genre WisdomGenre { get; set; }
    }
}
