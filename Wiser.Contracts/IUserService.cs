using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.User;
using Wiser.Models.Wisdom;

namespace Wiser.Contractst
{
    public interface IUserService
    {
        bool AddFavorite(WisdomFavoriteItem wisdomToFavorite);
        bool RemoveFavorite(WisdomFavoriteItem wisdomToUnfavorite);
        List<WisdomFavoriteItem> GetFavorites();
        List<WisdomContributionItem> GetContributions();
        bool RemoveUser(string id);
        List<UserScrollItem> GetUsers();
        bool ChangeRole(string id);
    }
}
