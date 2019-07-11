using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public bool AddFavorite(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userCheck = ctx.FavoriteTable.Where(u => u.UserId == userId && u.WisdomId == id).Count();
                if (userCheck > 0)
                {
                    return false;
                }
                ctx.FavoriteTable.Add(new Favorite()
                {
                    UserId = _userId,
                    WisdomId = id
                });
                ctx.SaveChanges();
                var saveCount = ctx.SaveChanges();
                return ctx.SaveChanges() == 1;
            }
        }
        public List<WisdomScrollItem> GetContributions()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .WisdomTable
                    .Where(w => w.UserId == _userId)
                    .Select(w => new WisdomScrollItem()
                    {
                        Content = w.Content,
                        Source = w.Source,
                        WisdomId = w.WisdomId,
                        ScrollAuthor = new AuthorScrollItem()
                        {
                            AuthorId = w.Author.AuthorId,
                            AuthorName = w.Author.FullName,
                            WisdomCount = w.Author.WisdomCount
                        }
                    }).ToList();
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
                        Content = a.Content,
                        IsUpvoted = a.IsUpvoted

                    }).ToList(),
                    Favorites = ctx.FavoriteTable.Where(w => w.UserId == user.Id).Select(f => new WisdomScrollItem()
                    {
                        FirstName = f.User.FirstName,
                        LastName = f.User.LastName,
                        ScrollAuthor = new AuthorScrollItem()
                        {
                            AuthorId = f.Wisdom.Author.AuthorId,
                            AuthorName = f.Wisdom.Author.FirstName + "" + f.Wisdom.Author.LastName,
                            WisdomCount = f.Wisdom.Author.WisdomCount
                        },
                        Source = f.Wisdom.Source,
                        Virtue = f.Wisdom.PostVirtue,
                        UserId = f.Wisdom.UserId,
                        WisdomId = f.Wisdom.WisdomId,
                        Content = f.Wisdom.Content,
                        IsUpvoted = f.Wisdom.IsUpvoted
                    }).ToList(),
                };

            }
        }
        public List<WisdomFavoriteItem> GetFavorites()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (List<WisdomFavoriteItem>)ctx
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
            using (var ctx = new ApplicationDbContext())
            {
                var favoriteToRemove = ctx.FavoriteTable.First(w => w.WisdomId == id && w.UserId == _userId);
                ctx.FavoriteTable.Remove(favoriteToRemove);
                return ctx.SaveChanges() == 1;
            }
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
                        Name = u.FirstName + " " + u.LastName,
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
                var newUpvote = new Upvoted()
                {
                    UserId = _userId,
                    WisdomId = wisdomId,
                    CreatedAt = DateTime.UtcNow
                };
                ctx.UpvotedTable.Add(newUpvote);
                ctx.SaveChanges();

                int virtuePostUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                return virtuePreUpdate != virtuePostUpdate;
            }
        }

        public List<UserDetailItem> TopUsers()
        {
            var listToReturn = new List<UserDetailItem>();
            using (var ctx = new ApplicationDbContext())
            {
                listToReturn = ctx
                    .Users
                    .Select(u => new UserDetailItem()
                    {
                        Name = u.FirstName + " " + u.LastName,
                        UserId = u.Id,
                        //Role = u.Roles.ToString(),
                        Virtue = u.Virtue,
                        Contributions = ctx.WisdomTable.Where(c => c.UserId == u.Id).Select(con => new WisdomScrollItem()
                        {
                            FirstName = con.User.FirstName,
                            LastName = con.User.LastName,
                            ScrollAuthor = new AuthorScrollItem()
                            {
                                AuthorId = con.AuthorId,
                                AuthorName = con.Author.FullName,
                                WisdomCount = con.Author.WisdomCount
                            },
                            Content = con.Content,
                            IsUpvoted = con.IsUpvoted,
                            Source = con.Source,
                            UserId = con.UserId,
                            Virtue = con.PostVirtue,
                            WisdomId = con.WisdomId
                        }).ToList(),
                        Favorites = ctx.FavoriteTable.Where(f => f.UserId == u.Id).Select(fav => new WisdomScrollItem()
                        {
                            FirstName = fav.User.FirstName,
                            LastName = fav.User.LastName,
                            ScrollAuthor = new AuthorScrollItem()
                            {
                                AuthorId = fav.Wisdom.Author.AuthorId,
                                AuthorName = fav.Wisdom.Author.FullName,
                                WisdomCount = fav.Wisdom.Author.WisdomCount
                            },
                            Content = fav.Wisdom.Content,
                            IsUpvoted = fav.Wisdom.IsUpvoted,
                            Source = fav.Wisdom.Source,
                            UserId = fav.Wisdom.UserId,
                            Virtue = fav.Wisdom.PostVirtue,
                            WisdomId = fav.Wisdom.WisdomId
                        }).ToList(),
                    }).ToList();
                return listToReturn.OrderByDescending(o => o.Virtue).ToList();
            };
        }

        public bool CheckUpvote(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var upvoteCheck = ctx.UpvotedTable.Where(u => u.UserId == userId && u.WisdomId == id);
                if (upvoteCheck.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CheckFavorite(int id, string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var favoriteCheck = ctx.FavoriteTable.Where(u => u.UserId == userId && u.WisdomId == id);
                if (favoriteCheck.Count() > 0)
                {
                    return true;
                }
                return false;
            }
        }
    }
}
