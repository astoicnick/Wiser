using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Favorite
{
    public class AddFavoriteItem
    {
        [Required]
        public int WisdomId { get; set; }
        [Required]
        public Guid UserId { get; set; }
    }
}
