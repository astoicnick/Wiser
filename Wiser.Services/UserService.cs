using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Contracts;
using Wiser.Models.Wisdom;

namespace Wiser.Services
{
    public class UserService : IUserService
    {
        public bool AddFavorite(WisdomFavoriteItem wisdomToFavorite)
        {
            throw new NotImplementedException();
        }

        public List<WisdomContributionItem> GetContributions()
        {
            throw new NotImplementedException();
        }

        public List<WisdomFavoriteItem> GetFavorites()
        {
            throw new NotImplementedException();
        }

        public bool RemoveFavorite(WisdomFavoriteItem wisdomToUnfavorite)
        {
            throw new NotImplementedException();
        }
    }
}
