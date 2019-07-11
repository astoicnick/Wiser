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
        bool CreateAuthor(AuthorCreateItem authorToCreate);
        AuthorDetailItem RetrieveAuthor(int idToRetrieve);
        bool UpdateAuthor(AuthorUpdateItem authorToUpdate);
        List<AuthorScrollItem> GetAuthors();
        bool DeleteAuthor(AuthorUpdateItem authorToDelete);
        List<AuthorDetailItem> GetDetailAuthors();
    }
}
