using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Wisdom;

namespace Wiser.Contracts
{
    public interface IWisdomService
    {
        bool CreateWisdom(WisdomCreateItem wisdomToCreate);
        bool RemoveWisdom(WisdomUpdateItem wisdomToRemove);
        WisdomDetailItem RetrieveWisdomById(int wisdomId);
        bool UpdateWisdom(WisdomUpdateItem wisdomToUpdate);
        List<WisdomScrollItem> GetWisdomList();
        bool Upvote(int wisdomId);
    }
}
