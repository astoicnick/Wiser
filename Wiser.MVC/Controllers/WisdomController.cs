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

            ModelState.AddModelError("", "Player could not be added.");

            return View(wisdomToCreate);
        }

        //Read all
        // GET: Wisdom
        public ActionResult Index()
        {
            _userId = User.Identity.GetUserId();
            _wisdomService = new WisdomService(_userId);
            return View(_wisdomService.GetWisdomList());
        }
        //Read Detail
        //GET: Wisdom/{id}
        public ActionResult Details(int id)
        {
            return DetailNullChecker(id);
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