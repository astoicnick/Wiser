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
    public class AuthorService : IAuthorService
    {
        public bool CreateAuthor(AuthorCreateItem authorToCreate)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.AuthorTable.Add(new Author() {
                    FirstName = authorToCreate.FirstName,
                    LastName = authorToCreate.LastName,
                   
                });
                ctx.AuthorTable.Add(entity);
                return ctx.SaveChanges() == 1;
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
                        AuthorName = a.FullName,
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
                    FirstName = query.FirstName,
                    LastName = query.LastName,
                    Virtue = query.Virtue,
                    Attributions = Attributions(query.AuthorId),
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
                return ctx.SaveChanges() == 1;
            }
        }
        public bool DeleteAuthor(AuthorUpdateItem authorToDelete)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.AuthorTable.Remove(ctx.AuthorTable.Find(authorToDelete.AuthorId));
                return ctx.SaveChanges() == 1;
            }
        }
        public List<WisdomAttributionItem> Attributions(int authorId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query = (List<WisdomAttributionItem>) ctx
                    .WisdomTable.
                    Where(w => w.AuthorId == authorId)
                    .Select(w => new WisdomAttributionItem() {
                        WisdomId = w.WisdomId,
                        CreatedAt = w.CreatedAt,
                        Content = w.Content,
                        Source = w.Source
                    });
                return query;
            }
        }

    }
}
