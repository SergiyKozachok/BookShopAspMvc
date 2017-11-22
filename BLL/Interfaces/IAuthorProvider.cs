using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IAuthorProvider
    {
        AuthorViewModel GetAuthorsByPage(int page, int pages, SearchAuthorsViewModel search);
        int AddAuthor(AddAuthorViewModel addAuthor);
        IEnumerable<AuthorItemViewModel> GetAuthors();
        void Delete(int id);
        EditAuthorViewModel EditAuthor(int id);
        int EditAuthor(EditAuthorViewModel editAuthor);
        AuthorItemViewModel GetAuthorInfo(int id);
        

    }
}
