using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wiser.Models.Author;
using Wiser.Models.Wisdom;

namespace Wiser.Contracts
{
    public interface IAuthorService
    {
        bool CreateAuthor(string firstName, string lastName);
        AuthorDetailItem RetrieveAuthor(int idToRetrieve);
        bool UpdateAuthor(AuthorUpdateItem authorToUpdate);
        List<AuthorScrollItem> GetAuthors();
        bool DeleteAuthor(AuthorUpdateItem authorToDelete);
        List<WisdomAttributionItem> Attributions(int authorId);
    }
}
