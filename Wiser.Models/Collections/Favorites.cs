using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Wisdom;

namespace Wiser.Models.Collections
{
    public class Favorites
    {
        //sample for what favorites should look like when implemented in user
        public Dictionary</*User*/Guid,WisdomScrollItem/*WisdomItem(Detail or Scroll?)*/> Favorite { get; set; }
    }
}
