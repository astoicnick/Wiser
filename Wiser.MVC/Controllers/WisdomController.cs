using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiser.Contracts;
using Wiser.Data;
using Wiser.Models.Wisdom;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [Authorize]
    public class WisdomController : Controller
    {
        private WisdomService _wisdomService;
        private AuthorService _authorService;
        private string _userId;

        //Create general
        //GET: Wisdom/Create
        public ActionResult Create()
        {
            _authorService = new AuthorService();
            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");
            return View();
        }
        //Create confirmed
        //POST: Wisdom/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create(WisdomCreateItem wisdomToCreate)
        {
            _authorService = new AuthorService();
            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");
            if (!ModelState.IsValid) return View(wisdomToCreate);

            _wisdomService = new WisdomService(User.Identity.GetUserId());

            if (_wisdomService.CreateWisdom(wisdomToCreate))
            {
                TempData["SaveResult"] = "Wisdom was Created!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Wisdom could not be added.");

            return View(wisdomToCreate);
        }

        //Read all
        // GET: Wisdom
        public ActionResult Index()
        {
            _userId = User.Identity.GetUserId();
            _wisdomService = new WisdomService(_userId);
            UserService userService = new UserService(_userId);
            TempData["Owner"] = userService.GetUsers().First(u => u.UserId == User.Identity.GetUserId()).Name;
            return View(_wisdomService.GetWisdomList());
        }
        //Hall of fame
        //GET: Wisdom/HallOfFame
        public ActionResult HallOfFame()
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            var wisdomSorted = _wisdomService.GetWisdomList().OrderByDescending(w => w.Virtue).ToList();
            return View(wisdomSorted);
        }
        //View by User
        //GET: Wisdom/User
        [ActionName("User")]
        public ActionResult ViewByUser()
        {
            return View(new UserService(User.Identity.GetUserId()).TopUsers().OrderBy(u=>u.Name).ToList());
        }
        //View by Author
        //GET: Wisdom/Author
        [ActionName("Author")]
        public ActionResult ViewByAuthor()
        {
            return View(new AuthorService().GetDetailAuthors().OrderBy(a=>a.FirstName).ToList());
        }
        //Read Detail
        //GET: Wisdom/{id}
        public ActionResult Details(int id)
        {
            return DetailNullChecker(id);
        }
        //Wisdom By User
        //GET: Wisdom/User/{id}
        public ActionResult GetByUser(string id)
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            return View(_wisdomService.GetWisdomList().Where(w => w.UserId == id));
        }
        //Wisdom by Author
        //GET: Wisdom/Author/{id}
        public ActionResult GetByAuthor(int id)
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            return View(_wisdomService.GetWisdomList().Where(w => w.ScrollAuthor.AuthorId == id).ToList());
        }
        //Update general
        //GET: Wisdom/Update/{id}
        public ActionResult Edit(int? id)
        {
            if (id == null) return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            _authorService = new AuthorService();
            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            var wisdomToCheck = _wisdomService.DetailToUpdateItem(_wisdomService.RetrieveWisdomById(id.Value));
                if (wisdomToCheck == null)
                {
                    return HttpNotFound();
                }
            return View(wisdomToCheck);
        }
        // Update confirmed
        //POST: Wisdom/Update/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult Edit(WisdomUpdateItem wisdomToUpdate)
        {
            _authorService = new AuthorService();
            ViewData["AuthorId"] = new SelectList(_authorService.GetAuthors(), "AuthorId", "AuthorName");

            _wisdomService = new WisdomService(User.Identity.GetUserId());
            if (_wisdomService.UpdateWisdom(wisdomToUpdate))
            {
                TempData["UpdateResult"] = "Wisdom Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(wisdomToUpdate);
        }
        //Manage Wisdom
        public ActionResult Manage()
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            if (_wisdomService.GetWisdomList().Where(w => w.UserId == User.Identity.GetUserId()) == null)
            {
                return RedirectToAction("Index", "Wisdom");
            }
            return View(_wisdomService.GetWisdomList().Where(w=>w.UserId == User.Identity.GetUserId()));
        }
        //Delete general
        //GET: Wisdom/Delete/{id}
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            var wisdomToCheck = _wisdomService.RetrieveWisdomById(id.Value);
            if (wisdomToCheck == null)
            {
                return HttpNotFound();
            }
            var wisdomToDisplay = _wisdomService.DetailToUpdateItem(wisdomToCheck);
            return View(wisdomToDisplay);
        }
        //Delete confirmed
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(WisdomUpdateItem wisdomToRemove)
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            if (_wisdomService.RemoveWisdom(wisdomToRemove))
            {
                TempData["RemoveResult"] = "Wisdom Removed Successfully!";
                return RedirectToAction("Index");
            }
            return View(wisdomToRemove);
        }
        private ActionResult DetailNullChecker(int id)
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            if (_wisdomService.RetrieveWisdomById(id) == null)
            {
                return HttpNotFound();
            }
            WisdomDetailItem modelDetailed = _wisdomService.RetrieveWisdomById(id);
            return View(modelDetailed);
        }
        
    }
}