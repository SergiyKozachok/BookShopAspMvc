using BLL.Interfaces;
using BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBookShop.Areas.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorProvider _authorProvider;

        public AuthorController(IAuthorProvider authorProvider)
        {
            _authorProvider = authorProvider;
        }

        //public ActionResult Index()
        //{
        //    var model = _authorProvider.GetAuthors();
        //    return View(model);
        //}

        public ActionResult Index(int page = 1, int pages = 10, SearchAuthorsViewModel search = null)
        {
            return View(_authorProvider.GetAuthorsByPage(page, pages, search));
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(AddAuthorViewModel author)
        {
            if (ModelState.IsValid)
            {
                var result = _authorProvider.AddAuthor(author);
                return RedirectToAction("Index");
            }

            
            return View(author);

        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _authorProvider.EditAuthor(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditAuthorViewModel editAuthor)
        {
            if (ModelState.IsValid)
            {
                int result = _authorProvider.EditAuthor(editAuthor);
                if (result == 0)
                    ModelState.AddModelError("", "Помилка збереження даних");
                else if (result != 0)
                    return RedirectToAction("Index");
            }
            return View(editAuthor);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var model = _authorProvider.GetAuthorInfo(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(AuthorItemViewModel productDel)
        {
            _authorProvider.Delete(productDel.Id);
            return RedirectToAction("Index");
        }
        // GET: Admin/Author
        
    }
}