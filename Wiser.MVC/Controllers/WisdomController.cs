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
        private string _userId;

        //Create general
        //GET: Wisdom/Create
        public ActionResult Create()
        {
            return View();
        }
        //Create confirmed
        //POST: Wisdom/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public ActionResult Create(WisdomCreateItem wisdomToCreate)
        {
           
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
        public ActionResult Edit(int id)
        {
            return DetailNullChecker(id);
        }
        // Update confirmed
        //POST: Wisdom/Update/{id}
        public ActionResult Edit(WisdomDetailItem wisdomToUpdate)
        {
            if (!ModelState.IsValid) return View(wisdomToUpdate);

            _wisdomService = new WisdomService(User.Identity.GetUserId());
            if (_wisdomService.UpdateWisdom(_wisdomService.DetailToUpdateItem(wisdomToUpdate)))
            {
                TempData["UpdateResult"] = "Wisdom Updated Successfully!";
                return RedirectToAction("Index");
            }
            return View(wisdomToUpdate);
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