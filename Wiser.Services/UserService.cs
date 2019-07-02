﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Contracts;
using Wiser.Data;
using Wiser.Models;
using Wiser.Models.Wisdom;

namespace Wiser.Services
{
    public class UserService : IUserService
    {
        private readonly string _userId;
        public UserService(string userId)
        {
            _userId = userId;
        }
        public bool AddFavorite(WisdomFavoriteItem wisdomToFavorite)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.FavoriteTable.Add(new Favorite()
                {
                    UserId = _userId,
                    User = ctx.Users.Find(wisdomToFavorite.UserId),
                    WisdomId = wisdomToFavorite.WisdomId,
                    Wisdom = ctx.WisdomTable.Find(wisdomToFavorite.WisdomId)
                });
                return ctx.SaveChanges() == 1;
            }
        }

        public List<WisdomContributionItem> GetContributions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (List<WisdomContributionItem>)ctx
                    .WisdomTable
                    .Where(w => w.UserId == _userId)
                    .Select(w => new WisdomContributionItem()
                    {
                        AuthorId = w.AuthorId,
                        AuthorName = w.Author.FullName,
                        Content = w.Content,
                        CreatedAt = w.CreatedAt,
                        Source = w.Source,
                        WisdomId = w.WisdomId,
                        WisdomGenre = w.WisdomGenre
                    });
                return query;
            }
        }

        public List<WisdomFavoriteItem> GetFavorites()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (List<WisdomFavoriteItem>) ctx
                    .FavoriteTable
                    .Where(w => w.UserId == _userId)
                    .Select(w => new WisdomFavoriteItem()
                    {
                        Content = w.Wisdom.Content,
                        Source = w.Wisdom.Source,
                        AuthorId = w.Wisdom.AuthorId,
                        AuthorName = w.Wisdom.Author.FullName,
                        UserId = _userId,
                        WisdomId = w.WisdomId
                    });
                return query;
            }
        }

        public bool RemoveFavorite(WisdomFavoriteItem wisdomToUnfavorite)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var favoriteToRemove = new Favorite()
                {
                    FavoriteId = wisdomToUnfavorite.FavoriteId,
                    UserId = wisdomToUnfavorite.UserId,
                    User = ctx.Users.Find(wisdomToUnfavorite.UserId),
                    WisdomId = wisdomToUnfavorite.WisdomId,
                    Wisdom = ctx.WisdomTable.Find(wisdomToUnfavorite.WisdomId)
                };
                ctx.FavoriteTable.Remove(favoriteToRemove);
                return ctx.SaveChanges() == 1;
            }
        }
    }
}