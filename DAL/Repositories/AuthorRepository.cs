using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using System.Data.Entity;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly IAppDBContext _context;

        public AuthorRepository(IAppDBContext context)
        {
            _context = context;
        }

        public Author Add(Author author)
        {
            _context.Set<Author>().Add(author);
            return author;
        }

        public void Delete(int id)
        {
            var author = this.GetAuthorById(id);
            if(author != null)
            {
                _context.Set<Author>().Remove(author);
                this.SaveChange();
            }
        }

        public void Dispose()
        {
            if (this._context != null)
                this._context.Dispose();
        }

        public IQueryable<Author> GetAllAuthors()
        {
            return _context.Set<Author>()
                .AsQueryable();
        }

        public Author GetAuthorById(int id)
        {
            return this.GetAllAuthors()
                .SingleOrDefault(c => c.Id == id);
        }

        public void SaveChange()
        {
            this._context.SaveChanges();
        }

        public int TotalAuthors()
        {
            return this.GetAllAuthors().Count();
        }
    }
}
