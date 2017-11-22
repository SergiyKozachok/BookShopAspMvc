using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IBookRepository : IDisposable
    {
        Book Add(Book book);
        IQueryable<Book> GetAllBooks();
        void Delete(int id);
        Book GetBookById(int id);
        void SaveChange();
        int TotalBooks();
    }
}
