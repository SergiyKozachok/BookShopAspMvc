using BLL.Interfaces;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookProvider _bookProvider;

        public BookController(IBookProvider bookProvider)
        {
            _bookProvider = bookProvider;
        }

        public ActionResult Index(int page = 1, int pages = 10, SearchBookViewModel search = null)
        {
            return View(_bookProvider.GetBooksByPage(page, pages, search));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddBookViewModel book)
        {
            if (ModelState.IsValid)
            {
                var result = _bookProvider.AddBook(book);
                return RedirectToAction("Index");
            }
            return View(book);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _bookProvider.EditBook(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditBookViewModel editBook)
        {
            if (ModelState.IsValid)
            {
                int result = _bookProvider.EditBook(editBook);
                if (result == 0)
                    ModelState.AddModelError("", "Помилка збереження даних");
                else if (result != 0)
                    return RedirectToAction("Index");
            }
            return View(editBook);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _bookProvider.GetBookInfo(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(BookItemViewModel bookDel)
        {
            _bookProvider.Delete(bookDel.Id);
            return RedirectToAction("Index");
        }
    }
}