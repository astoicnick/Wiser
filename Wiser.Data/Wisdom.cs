using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Data;

namespace Wiser.Data
{
    public enum Genre { Proverb, Quote, Fact, Musing }
    public class Wisdom
    {
        [DefaultValue(false)]
        public bool IsUpvoted { get; set; }
        //Keys and Ids
        [Key]
        public int WisdomId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        //Core properties
        [Required]
        public string Content { get; set; }
        //[Required]
        public Genre WisdomGenre { get; set; }
        //[Required]
        //How many upvotes
        public int PostVirtue { get; set; }
        [Required]
        public string Source { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}