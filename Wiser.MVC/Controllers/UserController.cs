using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiser.Data;
using Wiser.Models.User;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private UserService _userService;
        //Read general
        // GET: User
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            _userService = new UserService(User.Identity.GetUserId());
            var listOfUsers = _userService.GetUsers();
            return View(listOfUsers);
        }
        //Read Details
        //GET: User/{id}
        public ActionResult Details(string id)
        {
            return DetailNullChecker(id);
        }
        //Update general
        //GET: User/Edit/{id}
        [Authorize(Roles="Admin")]
        public ActionResult Edit(string id)
        {
            return DetailNullChecker(id);
        }
        //Update confirmed
        //POST: User/Edit/{id}
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult Edit(UserDetailItem userToEdit)
        {
            _userService = new UserService(User.Identity.GetUserId());
            if (_userService.RemoveUser(userToEdit.UserId))
            {
                TempData["UpdateResult"] = "User Removed Successfully";
                return RedirectToAction("Index");
            }
            return View(userToEdit);
        }

        //Delete general
        //GET: User/Delete/{id}
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(string id)
        {
            return DetailNullChecker(id);
        }
        //Delete confirmed
        //POST: User/Delete/{id}
        [HttpPost]
        [Authorize(Roles ="Admin")]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(UserDetailItem userToDelete)
        {
            _userService = new UserService(User.Identity.GetUserId());
            if (_userService.RemoveUser(userToDelete.UserId))
            {
                TempData["RemoveResult"] = "User Removed Successfully";
                //Should create admin portal at some point
                return RedirectToAction("Index");
            }
            return View(userToDelete);
        }

        //Change role general
        //GET: User/Changerole/{id}
        [HttpGet]
        public ActionResult Changerole(string id)
        {
            
            if (id =="")
            {
                return HttpNotFound();
            }
            return DetailNullChecker(id);
        }
        //Change role confirmed
        //POST: User/Changerole/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="Admin")]
        [ActionName("Changerole")]
        public ActionResult Changerole(UserDetailItem detail)
        {
            _userService = new UserService(User.Identity.GetUserId());
            if (_userService.ChangeRole(detail.UserId))
            {
                TempData["ChangeroleResult"] = "Role changed successfully";
                return RedirectToAction("Index");
            }
            return View(_userService.DetailedUser(detail.UserId));
        }
        private ActionResult DetailNullChecker(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _userService = new UserService(User.Identity.GetUserId());
            var userToCheck = _userService.DetailedUser(id);
            if (userToCheck == null)
            {
                return HttpNotFound();
            }
            return View(userToCheck);
        }
    }
}