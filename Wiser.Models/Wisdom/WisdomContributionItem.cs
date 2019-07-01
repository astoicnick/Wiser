using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wiser.Models.Wisdom
{
    public class WisdomContributionItem
    {
        public DateTime CreatedAt { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int WisdomId { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
    }
}
