using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Data
{
    public class Upvoted
    {
        [Key]
        public int PostId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Wisdom")]
        public int WisdomId { get; set; }
        public virtual Wisdom Wisdom { get; set; }
    }
}