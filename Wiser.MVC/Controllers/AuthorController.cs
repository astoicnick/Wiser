using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiser.Models.Author;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [Authorize]
    public class AuthorController : Controller
    {
        private AuthorService _authorService;
        //Create general
        //GET: Author/Create
        public ActionResult Create(int? id)
        {
            _authorService = new AuthorService();
            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");
            return View();
        }
        //Create confirmed
        //POST: Author/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create(AuthorCreateItem authorToCreate)
        {
            _authorService = new AuthorService();
            if (_authorService.CreateAuthor(authorToCreate))
            {
                TempData["CreateResult"] = "Author Created Successfully";
                return RedirectToAction("Index");
            }
            return View(authorToCreate);
        }
        //Read general
        // GET: Author
        public ActionResult Index()
        {
            _authorService = new AuthorService();
            return View(_authorService.GetAuthors());
        }
        //Read DetailedView
        //GET: Author/{id}
        public ActionResult Details(int? id)
        {
            return DetailNullChecker(id);
        }
        //Hall of fame(Top 3 Authors then the rest sorted by virtue)
        public ActionResult HallOfFame()
        {
            _authorService = new AuthorService();
            var authors = _authorService.GetDetailAuthors();
            return View(authors);
        }

        //Edit general
        //GET: Author/Edit/{id}
        [Authorize(Roles ="Admin")]
        public ActionResult Edit(int? id)
        {
            _authorService = new AuthorService();

            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(),"AuthorId","AuthorName");
            if (_authorService.RetrieveAuthor(id.Value) == null)
            {
                return HttpNotFound();
            }
            return View(_authorService.DetailToUpdate(_authorService.RetrieveAuthor(id.Value)));
        }
        //Edit confirmed
        //POST: Author/Edit/{id}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        //IF user is admin show buttons
        public ActionResult Edit(AuthorUpdateItem authorToUpdate)
        {
            _authorService = new AuthorService(); ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");
            if (_authorService.UpdateAuthor(authorToUpdate))
            {
                TempData["UpdateResult"] = "Author Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(authorToUpdate);
        }

        //Delete general
        //GET: Author/Delete/{id}
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            _authorService = new AuthorService();
            if (!id.HasValue)
            {
                return HttpNotFound();
            }
            AuthorUpdateItem authorToDelete = _authorService.DetailToUpdate(_authorService.RetrieveAuthor(id.Value));
            return View(authorToDelete);

        }
        //Delete confirm
        //POST: Author/Delete/{id}
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(AuthorUpdateItem authorToDelete)
        {
            _authorService = new AuthorService();
            if (_authorService.DeleteAuthor(authorToDelete))
            {
                TempData["DeleteResult"] = "Author deleted successfully";
                return RedirectToAction("Index");
            }
            return View(authorToDelete);
        }

        private ActionResult DetailNullChecker(int? id)
        {
            _authorService = new AuthorService();
            if (_authorService.RetrieveAuthor(id.Value) == null)
            {
                return HttpNotFound();
            }
            AuthorDetailItem modelDetailed = _authorService.RetrieveAuthor(id.Value);
            return View(modelDetailed);
        }
    }
}