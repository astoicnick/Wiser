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
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            _userService = new UserService(User.Identity.GetUserId());
            var listOfUsers = _userService.GetUsers();
            return View(listOfUsers);
        }

        //Public view of Users
        [Authorize]
        public ActionResult TopUsers()
        {
            _userService = new UserService(User.Identity.GetUserId());
            return View(_userService.TopUsers());
        }
        //Get just contributions
        public ActionResult UserContributions()
        {
            _userService = new UserService(User.Identity.GetUserId());
            return View(_userService.GetContributions());
        }
        //Read Details
        //GET: User/{id}
        public ActionResult Details(string id)
        {
            return DetailNullChecker(id);
        }
        //Update general
        //GET: User/Edit/{id}
        public ActionResult Edit(string id)
        {
            if (id != User.Identity.GetUserId())
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            _userService = new UserService(User.Identity.GetUserId());
            var user = _userService.GetEditItem(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }
        //Update confirmed
        //POST: User/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Edit")]
        public ActionResult Edit(UserEditItem userToEdit)
        {
            if (userToEdit.UserId == User.Identity.GetUserId())
            {


                _userService = new UserService(User.Identity.GetUserId());
                if (_userService.EditUser(userToEdit))
                {
                    TempData["UpdateResult"] = "User Updated Successfully";
                    return RedirectToAction("Index");
                }
                return View(userToEdit);
            }
            TempData["Unauthorized"] = "You are not authorized to use this function";
            return RedirectToAction("Index");
        }

        //Delete general
        //GET: User/Delete/{id}
        [Authorize(Roles ="Admin")]
        public ActionResult Delete(string id)
        {
            if (id == User.Identity.GetUserId())
            {
                return DetailNullChecker(id);
            }
            TempData["Unauthorized"] = "You are not authorized to use this function";
            return RedirectToAction("Index","Wisdom");
        }
        //Delete confirmed
        //POST: User/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public ActionResult Delete(UserDetailItem userToDelete)
        {
            if (userToDelete.UserId == User.Identity.GetUserId())
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
            TempData["Unauthorized"] = "You are not authorized to use this function";
            return RedirectToAction("Index", "Wisdom");
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