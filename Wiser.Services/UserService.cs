using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Contracts;
using Wiser.Contracts;
using Wiser.Data;
using Wiser.Models;
using Wiser.Models.Author;
using Wiser.Models.User;
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
        public bool AddFavorite(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userCheck = ctx.FavoriteTable.Where(u => u.UserId == _userId && u.WisdomId == id);
                if (userCheck.Count() > 0)
                {
                    return false;
                }
                ctx.FavoriteTable.Add(new Favorite()
                {
                    UserId = _userId,
                    WisdomId = id
                });
                // var saveCount == ctx.SaveChanges();
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
        public UserDetailItem DetailedUser(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx
                    .Users
                    .Find(id);
                return new UserDetailItem()
                {
                    UserId = user.Id,
                    Name = user.FirstName + " " + user.LastName,
                    Contributions = ctx.WisdomTable.Where(w => w.UserId == user.Id).Select(a => new WisdomScrollItem()
                    {
                        FirstName = a.User.FirstName,
                        LastName = a.User.LastName,
                        ScrollAuthor = new AuthorScrollItem()
                        {
                            AuthorId = a.AuthorId,
                            AuthorName = a.Author.FirstName + "" + a.Author.LastName,
                            WisdomCount = a.Author.WisdomCount
                        },
                        Source = a.Source,
                        Virtue = a.PostVirtue,
                        UserId = a.UserId,
                        WisdomId = a.WisdomId,
                        Content = a.Content
                    }).ToList()
                };

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

        public bool RemoveFavorite(int id)
        {
            return true;
            //using (var ctx = new ApplicationDbContext())
            //{
            //    var favoriteToRemove = new Favorite()
            //    {
            //        FavoriteId = wisdomToUnfavorite.FavoriteId,
            //        UserId = wisdomToUnfavorite.UserId,
            //        User = ctx.Users.Find(wisdomToUnfavorite.UserId),
            //        WisdomId = wisdomToUnfavorite.WisdomId,
            //        Wisdom = ctx.WisdomTable.Find(wisdomToUnfavorite.WisdomId)
            //    };
            //    ctx.FavoriteTable.Remove(favoriteToRemove);
            //    return ctx.SaveChanges() == 1;
            //}
        }

        public bool RemoveUser(string id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Users.Remove(ctx.Users.Find(id));
                return ctx.SaveChanges() == 1;
            }
        }

        public List<UserScrollItem> GetUsers()
        {
            var listToReturn = new List<UserScrollItem>();
            using (var ctx = new ApplicationDbContext())
            {
                listToReturn = ctx
                    .Users
                    .Select(u => new UserScrollItem()
                    {
                        Name = u.FirstName+" "+u.LastName,
                        UserId = u.Id
                    }).ToList();
                return listToReturn;
            };
        }

        public bool EditUser(UserEditItem userToEdit)
        {
            if (userToEdit.UserId == _userId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var toChange = ctx.Users.Find(userToEdit.UserId);
                    toChange.Email = userToEdit.Email;
                    toChange.FirstName = userToEdit.FirstName;
                    toChange.LastName = userToEdit.LastName;
                    toChange.PhoneNumber = userToEdit.PhoneNumber;
                    toChange.UserName = userToEdit.UserName;
                    return ctx.SaveChanges() == 1;
                }
            }
            return false;
        }
        public UserEditItem GetEditItem(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var user = ctx.Users.Find(userId);

                return new UserEditItem()
                {
                    UserId = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName
                };
            }
        }
        public bool Upvote(int wisdomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                //null obj
                var upvoteCount = ctx.UpvotedTable.Where(u => u.WisdomId == wisdomId && u.UserId == _userId);
                if (upvoteCount.Count() > 0)
                {
                    return false;
                }
                var wisdomToUpvote = ctx.WisdomTable.Single(w => w.WisdomId == wisdomId);
                int virtuePreUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                wisdomToUpvote.PostVirtue += 1;
                wisdomToUpvote.Author.Virtue += 1;
                wisdomToUpvote.User.Virtue += 1;
                ctx.UpvotedTable.Add(new Upvoted()
                {
                    UserId = _userId,
                    WisdomId = wisdomId,
                    CreatedAt = DateTime.UtcNow
                });
                ctx.SaveChanges();

                int virtuePostUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                return virtuePreUpdate != virtuePostUpdate;
            }
        }
    }
}
