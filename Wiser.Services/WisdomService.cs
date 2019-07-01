using System;
using System.Collections.Generic;
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
        private readonly Guid _userId;
        public WisdomService(Guid userId)
        {
            _userId = userId;
        }
        public bool CreateWisdom(WisdomCreateItem wisdomToCreate)
        {

            var entity =
                new Wisdom()
                {
                    UserId = _userId,
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
                        LastName = wisdomToCreate.AuthorLastName
                    };
                    ctx.AuthorTable.Add(newAuthor);
                    ctx.SaveChanges();
                }
                entity.Author = ctx.AuthorTable.Find(wisdomToCreate.AuthorId);
                ctx.WisdomTable.Add(entity);
                if (wisdomToCreate.AuthorId == null)
                {
                    return ctx.SaveChanges() == 2;
                }
                return ctx.SaveChanges() == 1;
            }


        }

        public List<WisdomScrollItem> GetWisdomList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (List<WisdomScrollItem>)
                    ctx
                        .WisdomTable
                        .Select(
                        e =>
                            new WisdomScrollItem
                            {
                                Virtue = e.PostVirtue,
                                UserId = e.UserId,
                                UserName = e.User.FullName,
                                Content = e.Content,
                                Source = e.Source,
                                Author = new AuthorScrollItem()
                                {
                                    AuthorId = e.AuthorId,
                                    AuthorName = e.Author.FullName,
                                    WisdomCount = ctx.WisdomTable.Where(a => a.AuthorId == e.AuthorId).Count()
                                }
                            });
                return query;
            }
        }

        public bool RemoveWisdom(WisdomUpdateItem wisdomToRemove)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.WisdomTable.Remove(ctx.WisdomTable.Find(wisdomToRemove.WisdomId));
                return ctx.SaveChanges() == 1;
            }
        }

        public WisdomDetailItem RetrieveWisdomById(int wisdomId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (WisdomDetailItem) ctx.WisdomTable
                    .Where(w => w.WisdomId == wisdomId)
                    .Select(e => new WisdomDetailItem
                    {
                        UserId = e.UserId,
                        UserName = e.User.FullName,
                        Author = new AuthorScrollItem
                        {
                            AuthorId = e.AuthorId,
                            WisdomCount = ctx.WisdomTable.Where(w => w.AuthorId == e.AuthorId).Count(),
                            AuthorName = e.Author.FullName
                        },
                        Content = e.Content,
                        Source = e.Source,
                        WisdomGenre = e.WisdomGenre,
                        CreatedAt = e.CreatedAt,
                        Virtue = e.PostVirtue
                    });
                return query;
            }
        }

        public bool UpdateWisdom(WisdomUpdateItem wisdomToUpdate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var toUpdate =
                    ctx
                    .WisdomTable
                    .Single(e => e.WisdomId == wisdomToUpdate.WisdomId && e.UserId == wisdomToUpdate.UserId);
                toUpdate.AuthorId = wisdomToUpdate.AuthorId;
                toUpdate.Content = wisdomToUpdate.Content;
                toUpdate.Source = wisdomToUpdate.Source;
                toUpdate.WisdomGenre = wisdomToUpdate.WisdomGenre;
                return ctx.SaveChanges() == 1;
            }
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
                int virtuePostUpdate = (int)(wisdomToUpvote.User.Virtue) + (int)(wisdomToUpvote.Author.Virtue) + (int)(wisdomToUpvote.PostVirtue);
                return virtuePreUpdate != virtuePostUpdate;
            }
        }
    }
}
