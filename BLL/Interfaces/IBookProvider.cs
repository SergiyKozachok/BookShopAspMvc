using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IBookProvider
    {
        BookViewModel GetBooksByPage(int page, int pages, SearchBookViewModel search);
        int AddBook(AddBookViewModel addBook);
        IEnumerable<BookItemViewModel> GetBooks();
        void Delete(int id);
        EditBookViewModel EditBook(int id);
        int EditBook(EditBookViewModel editBook);
        BookItemViewModel GetBookInfo(int id);
    }
}
