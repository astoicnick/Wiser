using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Data;
using Wiser.Models;
using Wiser.Models.Author;
using Wiser.Models.Wisdom;

namespace Wiser.Services
{
    public class AuthorService
    {
        public bool CreateAuthor(AuthorCreateItem authorToCreate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                if (authorToCreate.LastName == null)
                {
                    var entity = new Author()
                    {
                        FirstName = authorToCreate.FirstName,
                        FullName = authorToCreate.FirstName,
                        CreatedAt = DateTime.Now
                    };
                    if (entity.FullName != null)
                    {
                        ctx.AuthorTable.Add(entity);
                        return ctx.SaveChanges() == 1;
                    }
                    return false;
                }
                else
                {
                    var entity = new Author()
                    {
                        FirstName = authorToCreate.FirstName,
                        LastName = authorToCreate.LastName,
                        FullName = authorToCreate.FirstName + " " + authorToCreate.LastName,
                        Virtue = 0,
                        WisdomCount = 0,
                        CreatedAt = DateTime.Now
                    };
                    ctx.AuthorTable.Add(entity);
                    return ctx.SaveChanges() == 1;
                }
            }
        }
        public List<AuthorScrollItem> GetAuthors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .AuthorTable
                    .Select(a => new AuthorScrollItem()
                    {
                        AuthorId = a.AuthorId,
                        AuthorName = a.FirstName+" "+a.LastName,
                        WisdomCount = a.WisdomCount
                    })
                    .ToList();
                return query;
            }
        }
        public AuthorDetailItem RetrieveAuthor(int idToRetrieve)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (Author)ctx
                    .AuthorTable
                    .Single(author => author.AuthorId == idToRetrieve);
                var authorToReturn = new AuthorDetailItem()
                {
                    WisdomCount = query.WisdomCount,
                    AuthorId = query.AuthorId,
                    FirstName = query.FirstName,
                    LastName = query.LastName,
                    Virtue = query.Virtue,
                    Attributions = ctx.WisdomTable.Where(w=>w.AuthorId == query.AuthorId).Select(w=> new WisdomScrollItem()
                    {
                        FirstName = w.Author.FirstName,
                        LastName = w.Author.LastName,
                        ScrollAuthor = new AuthorScrollItem()
                        {
                            AuthorId = w.AuthorId,
                            AuthorName = w.Author.FullName,
                            WisdomCount = w.Author.WisdomCount
                        },
                        Content = w.Content,
                        Source = w.Source,
                        IsUpvoted = w.IsUpvoted,
                        UserId = w.UserId,
                        Virtue = w.PostVirtue,
                        WisdomId = w.WisdomId
                    }).ToList(),
                    CreatedAt = query.CreatedAt
                };
                return authorToReturn;
            }
        }
        public bool UpdateAuthor(AuthorUpdateItem authorToUpdate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .AuthorTable
                    .Single(author => author.AuthorId == authorToUpdate.AuthorId);
                query.FirstName = authorToUpdate.FirstName;
                query.LastName = authorToUpdate.LastName;
                query.FullName = authorToUpdate.FullName;
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAuthor(AuthorUpdateItem authorToDelete)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var authortoDeleteAuthorItem = ctx.AuthorTable.Find(authorToDelete.AuthorId);
                ctx.AuthorTable.Remove(ctx.AuthorTable.Find(authorToDelete.AuthorId));
                return ctx.SaveChanges() == 1;
            }
        }
        public AuthorUpdateItem DetailToUpdate(AuthorDetailItem detail)
        {
            return new AuthorUpdateItem()
            {
                AuthorId = detail.AuthorId,
                CreatedAt = detail.CreatedAt,
                FirstName = detail.FirstName,
                LastName = detail.LastName
            };
        }

        public List<AuthorDetailItem> GetDetailAuthors()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = ctx
                    .AuthorTable
                    .Select(a => new AuthorDetailItem()
                    {
                        WisdomCount = a.WisdomCount,
                       AuthorId = a.AuthorId,
                       CreatedAt = a.CreatedAt,
                       FirstName = a.FirstName,
                       LastName = a.LastName,
                       Virtue = a.Virtue,
                        Attributions = ctx.WisdomTable.Where(w => w.AuthorId == a.AuthorId).Select(w => new WisdomScrollItem()
                        {
                            FirstName = w.Author.FirstName,
                            LastName = w.Author.LastName,
                            ScrollAuthor = new AuthorScrollItem()
                            {
                                AuthorId = w.AuthorId,
                                AuthorName = w.Author.FullName,
                                WisdomCount = w.Author.WisdomCount
                            },
                            Content = w.Content,
                            Source = w.Source,
                            IsUpvoted = w.IsUpvoted,
                            UserId = w.UserId,
                            Virtue = w.PostVirtue,
                            WisdomId = w.WisdomId
                        }).ToList(),
                    })
                    .ToList();
                return query.OrderByDescending(q=>q.Virtue).ToList();
            }
        }
    }
}