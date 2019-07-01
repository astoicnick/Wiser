using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Contracts;
using Wiser.Models.Author;
using Wiser.Models.Wisdom;

namespace Wiser.Services
{
    public class AuthorService : IAuthorService
    {
        public List<WisdomAttributionItem> Attributions(int authorId)
        {
            throw new NotImplementedException();
        }

        public bool CreateAuthor(string firstName, string lastName)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAuthor(AuthorUpdateItem authorToDelete)
        {
            throw new NotImplementedException();
        }

        public List<AuthorScrollItem> GetAuthors()
        {
            throw new NotImplementedException();
        }

        public AuthorDetailItem RetrieveAuthor(int idToRetrieve)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAuthor(AuthorUpdateItem authorToUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
