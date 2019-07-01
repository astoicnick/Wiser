using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Data
{
    public class Favorite
    {
        [Key]
        public int FavoriteId { get; set; }
        //Don't even need foreign key.. wow!
        public User User { get; set; }

        [ForeignKey("Wisdom")]
        public int WisdomId { get; set; }
        public virtual Wisdom Wisdom { get; set; }
    }
}