using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class AuthorProvider : IAuthorProvider
    {
        IAuthorRepository _authorRepository;

        public AuthorProvider(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public int AddAuthor(AddAuthorViewModel addAuthor)
        {
            Author author = new Author
            {
                FirstName = addAuthor.FirstName,
                LastName = addAuthor.LastName
            };
            _authorRepository.Add(author);
            _authorRepository.SaveChange();
            return author.Id;
        }

        public void Delete(int id)
        {
            _authorRepository.Delete(id);
        }

        public EditAuthorViewModel EditAuthor(int id)
        {
            EditAuthorViewModel model = null;

            var author = _authorRepository.GetAuthorById(id);
            if(author != null)
            {
                model = new EditAuthorViewModel
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
            }
            return model;
        }

        public int EditAuthor(EditAuthorViewModel editAuthor)
        {
            try
            {
                var author =
                    _authorRepository.GetAuthorById(editAuthor.Id);
                if(author != null)
                {
                    author.FirstName = editAuthor.FirstName;
                    author.LastName = editAuthor.LastName;
                    _authorRepository.SaveChange();
                }
            }
            catch
            {
                return 0;
            }
            return editAuthor.Id;
        }

        public AuthorItemViewModel GetAuthorInfo(int id)
        {
            AuthorItemViewModel model = null;
            var author = _authorRepository.GetAuthorById(id);
            if(author != null)
            {
                model = new AuthorItemViewModel
                {
                    Id = author.Id,
                    FirstName = author.FirstName,
                    LastName = author.LastName
                };
            }
            return model;
        }

        public IEnumerable<AuthorItemViewModel> GetAuthors()
        {
            var model = _authorRepository.GetAllAuthors()
                .Select(c => new AuthorItemViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                });
            return model.AsEnumerable();
        }

        public AuthorViewModel GetAuthorsByPage(int page, int pages, SearchAuthorsViewModel search)
        {
            IQueryable<Author> query = _authorRepository.GetAllAuthors();
            AuthorViewModel model = new AuthorViewModel();

            if (!string.IsNullOrEmpty(search.FirstName))
            {
                query = query.Where(c => c.FirstName.Contains(search.FirstName)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(search.LastName))
            {
                query = query.Where(c => c.LastName.Contains(search.LastName)).AsQueryable();
            }

            model.Authors = query
                .OrderBy(c => c.LastName)
                .Skip((page - 1) * pages)
                .Take(pages)
                .Select(c => new AuthorItemViewModel
                {
                    Id = c.Id,
                    FirstName = c.FirstName,
                    LastName = c.LastName
                }).AsQueryable();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / pages);
            model.CurrentPage = page;
            model.Search = search;
            model.Pages = pages;

            return model;
        }
    }
}
