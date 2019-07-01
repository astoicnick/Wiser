using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Favorite
{
    public class AddFavoriteItem
    {
        public int WisdomId { get; set; }
        public Guid UserId { get; set; }
    }
}
