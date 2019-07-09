using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.User;
using Wiser.Models.Wisdom;

namespace Wiser.Contracts
{
    public interface IUserService
    {
        bool Upvote(int wisdomId);
        bool AddFavorite(int id);
        bool RemoveFavorite(int id);
        List<WisdomFavoriteItem> GetFavorites();
        List<WisdomContributionItem> GetContributions();
        bool RemoveUser(string id);
        List<UserScrollItem> GetUsers();
        bool EditUser(UserEditItem userToEdit);
        UserEditItem GetEditItem(string userId);
    }
}
