﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Contracts;
using Wiser.Data;
using Wiser.Models;
using Wiser.Models.Author;
using Wiser.Models.Wisdom;

namespace Wiser.Services
{
    public class WisdomService : IWisdomService
    {
        private readonly string _userId;
        public WisdomService(string userId)
        {
            _userId = userId;
        }
        public bool CreateWisdom(WisdomCreateItem wisdomToCreate)
        {
            var entity =
                new Wisdom()
                {
                    UserId = this._userId,
                    Content = wisdomToCreate.Content,
                    WisdomGenre = wisdomToCreate.WisdomGenre,
                    Source = wisdomToCreate.Source,
                    CreatedAt = DateTime.Now
                };
            using (var ctx = new ApplicationDbContext())
            {
                if (wisdomToCreate.AuthorId == null)
                {
                    var newAuthor = new Author()
                    {
                        FirstName = wisdomToCreate.AuthorFirstName,
                        LastName = wisdomToCreate.AuthorLastName,
                        WisdomCount = 1,
                        Virtue = 0
                    };
                    newAuthor.FullName = $"{newAuthor.FirstName} {newAuthor.LastName}";
                    ctx.AuthorTable.Add(newAuthor);
                    entity.Author = newAuthor;
                    ctx.SaveChanges();
                    entity.Author.WisdomCount += 1;
                    entity.WisdomId = ctx.WisdomTable.Count() + 1;
                    if ((entity.User = ctx.Users.Find(entity.UserId)) == null)
                    {
                        return false;
                    }
                    ctx.WisdomTable.Add(entity);
                    return ctx.SaveChanges() == 2;
                }
                else {
                    entity.AuthorId = wisdomToCreate.AuthorId.Value;
                    entity.Author = ctx.AuthorTable.Find(entity.AuthorId);
                    entity.Author.WisdomCount += 1;
                    entity.WisdomId = ctx.WisdomTable.Count() + 1;
                    if ((entity.User = ctx.Users.Find(entity.UserId)) == null)
                    {
                        return false;
                    }
                    ctx.WisdomTable.Add(entity);
                    var changes = ctx.SaveChanges();
                    return changes == 2;
                }
            }
        }

        public List<WisdomScrollItem> GetWisdomList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .WisdomTable
                        .Select(
                        e =>
                            new WisdomScrollItem
                            {
                                WisdomId = e.WisdomId,
                                Virtue = e.PostVirtue,
                                UserId = e.UserId,
                                FirstName = e.User.FirstName,
                                LastName = e.User.LastName,
                                Content = e.Content,
                                Source = e.Source,
                                ScrollAuthor = new AuthorScrollItem()
                                {
                                    AuthorId = e.Author.AuthorId,
                                    AuthorName = e.Author.FullName,
                                    WisdomCount = e.Author.WisdomCount
                                }

                            }).ToList();
                return query;
            }
        }

        public bool RemoveWisdom(WisdomUpdateItem wisdomToRemove)
        {
            if (_userId == wisdomToRemove.UserId)
            {

                using (var ctx = new ApplicationDbContext())
                {
                    var wisdom = ctx.WisdomTable.Find(wisdomToRemove.WisdomId);
                    wisdom.User.Virtue -= wisdom.PostVirtue;
                    wisdom.Author.WisdomCount -= 1;
                    ctx.WisdomTable.Remove(ctx.WisdomTable.Find(wisdomToRemove.WisdomId));
                    return ctx.SaveChanges() == 1;
                }
            }
            else return false;
        }

        public WisdomDetailItem RetrieveWisdomById(int wisdomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .WisdomTable
                    .Single(w => w.WisdomId == wisdomId);
                return new WisdomDetailItem()
                {
                    UserId = query.UserId,
                        FirstName = query.User.FirstName,
                        LastName = query.User.LastName,
                        Author = new AuthorScrollItem
                        {
                            AuthorId = query.AuthorId,
                            WisdomCount = ctx.WisdomTable.Where(a => a.AuthorId == query.AuthorId).Count(),
                            AuthorName = query.Author.FullName
                        },
                        Content = query.Content,
                        Source = query.Source,
                        WisdomGenre = query.WisdomGenre,
                       CreatedAt = query.CreatedAt,
                        Virtue = query.PostVirtue,
                        WisdomId = query.WisdomId
                };
            }
        }

        public bool UpdateWisdom(WisdomUpdateItem wisdomToUpdate)
        {
            if (_userId == wisdomToUpdate.UserId)
            {
                using (var ctx = new ApplicationDbContext())
                {
                    var toUpdate =
                        ctx
                        .WisdomTable
                        .Single(e => e.WisdomId == wisdomToUpdate.WisdomId && e.UserId == wisdomToUpdate.UserId);
                    toUpdate.Author.WisdomCount -= 1;
                    ctx.SaveChanges();
                    ctx.Entry(toUpdate).CurrentValues.SetValues(wisdomToUpdate);
                    toUpdate.Author.WisdomCount += 1;
                    ctx.SaveChanges();
                    var saveCount = ctx.SaveChanges();
                    return ctx.SaveChanges() == saveCount;
                }
            }
            return false;
        }

        public bool Upvote(int wisdomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var wisdomToUpvote = ctx.WisdomTable.Single(w => w.WisdomId == wisdomId);
                int virtuePreUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                wisdomToUpvote.PostVirtue += 1;
                wisdomToUpvote.Author.Virtue += 1;
                wisdomToUpvote.User.Virtue += 1;
                ctx.Users.Find(_userId).VirtueToGiveToday -= 1;

                int virtuePostUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                return virtuePreUpdate != virtuePostUpdate;
            }
        }
        public WisdomUpdateItem DetailToUpdateItem(WisdomDetailItem detailItem)
        {
            return new WisdomUpdateItem()
            {
                AuthorId = detailItem.Author.AuthorId,
                AuthorName = detailItem.Author.AuthorName,
                Content = detailItem.Content,
                Source = detailItem.Source,
                UserId = detailItem.UserId,
                WisdomGenre = detailItem.WisdomGenre,
                WisdomId = detailItem.WisdomId
            };
        }
    }
}
