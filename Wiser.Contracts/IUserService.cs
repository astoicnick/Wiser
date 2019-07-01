using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Wisdom;

namespace Wiser.Contracts
{
    public interface IUserService
    {
        bool AddFavorite(WisdomFavoriteItem wisdomToFavorite);
        bool RemoveFavorite(WisdomFavoriteItem wisdomToUnfavorite);
        List<WisdomFavoriteItem> GetFavorites();
        List<WisdomContributionItem> GetContributions();
    }
}
