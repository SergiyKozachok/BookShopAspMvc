using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.ViewModels;
using DAL.Interfaces;
using DAL.Entities;

namespace BLL.Services
{
    public class BookProvider : IBookProvider
    {
        public IBookRepository _bookRepository;

        public BookProvider(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public int AddBook(AddBookViewModel addBook)
        {
            Book book = new Book
            {
                Title = addBook.Title,
                Pages = addBook.Pages,
                AuthorId = addBook.AuthorId
            };
            _bookRepository.Add(book);
            _bookRepository.SaveChange();
            return book.Id;
        }

        public void Delete(int id)
        {
            _bookRepository.Delete(id);
        }

        public EditBookViewModel EditBook(int id)
        {
            EditBookViewModel model = null;

            var book = _bookRepository.GetBookById(id);
            if (book != null)
            {
                model = new EditBookViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Pages = book.Pages,
                    AuthorId = book.AuthorId
                };
            }
            return model;
        }

        public int EditBook(EditBookViewModel editBook)
        {
            try
            {
                var book =
                    _bookRepository.GetBookById(editBook.Id);
                if (book != null)
                {
                    book.Title = editBook.Title;
                    book.Pages = editBook.Pages;
                    book.AuthorId = editBook.AuthorId;
                    _bookRepository.SaveChange();
                }
            }
            catch
            {
                return 0;
            }
            return editBook.Id;
        }

        public BookItemViewModel GetBookInfo(int id)
        {
            BookItemViewModel model = null;
            var book = _bookRepository.GetBookById(id);
            if (book != null)
            {
                model = new BookItemViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Pages = book.Pages,
                    Author = book.Author.LastName
                };
            }
            return model;
        }

        public IEnumerable<BookItemViewModel> GetBooks()
        {
            var model = _bookRepository.GetAllBooks()
                .Select(c => new BookItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Pages = c.Pages,
                    Author = c.Author.LastName
                });
            return model.AsEnumerable();
        }

        public BookViewModel GetBooksByPage(int page, int pages, SearchBookViewModel search)
        {
            IQueryable<Book> query = _bookRepository.GetAllBooks();
            BookViewModel model = new BookViewModel();

            if (!string.IsNullOrEmpty(search.Title))
            {
                query = query.Where(c => c.Title.Contains(search.Title)).AsQueryable();
            }

            if (!string.IsNullOrEmpty(search.Author))
            {
                query = query.Where(c => c.Author.LastName.Contains(search.Author)).AsQueryable();
            }

            model.Books = query
                .OrderBy(c => c.Title)
                .Skip((page - 1) * pages)
                .Take(pages)
                .Select(c => new BookItemViewModel
                {
                    Id = c.Id,
                    Title = c.Title,
                    Pages = c.Pages,
                    Author = c.Author.LastName
                }).AsQueryable();
            model.TotalPages = (int)Math.Ceiling((double)query.Count() / pages);
            model.CurrentPage = page;
            model.Search = search;
            model.Pages = pages;

            return model;
        }
    }
}
