using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Repositories
{
    public class BookRepository : SqlRepository, IBookRepository
    {
        private readonly IAppDBContext _context;

        public BookRepository(IAppDBContext context)
            : base(context)
        {
            _context = context;
        }

        public Book Add(Book book)
        {
            this.Insert(book);
            this.SaveChanges();
            return book;
        }

        public void Delete(int id)
        {
            var book = this.GetBookById(id);
            if (book != null)
            {
                _context.Set<Book>().Remove(book);
                this.SaveChange();
            }
        }

        public void Dispose()
        {
            if (this._context != null)
                this._context.Dispose();
        }

        public IQueryable<Book> GetAllBooks()
        {
            return _context.Set<Book>()
                .AsQueryable();
        }

        public Book GetBookById(int id)
        {
            return this.GetAllBooks()
                .SingleOrDefault(c => c.Id == id);
        }

        public void SaveChange()
        {
            this._context.SaveChanges();
        }

        public int TotalBooks()
        {
            return this.GetAllBooks().Count();
        }
    }
}
