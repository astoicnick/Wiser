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
        bool AddFavorite(int id, string userId);
        bool RemoveFavorite(int id);
        List<WisdomFavoriteItem> GetFavorites();
        List<WisdomScrollItem> GetContributions();
        bool RemoveUser(string id);
        List<UserScrollItem> GetUsers();
        List<UserDetailItem> TopUsers();
        bool EditUser(UserEditItem userToEdit);
        UserEditItem GetEditItem(string userId);
        bool CheckUpvote(int id, string userId);
        bool CheckFavorite(int id, string userId);
    }
}
