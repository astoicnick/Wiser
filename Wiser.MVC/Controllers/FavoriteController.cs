using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [Authorize]
    public class FavoriteController : Controller
    {
        private UserService _userService;
        private string _userId;
        // GET: Favorite
        public ActionResult Index()
        {
            _userId = User.Identity.GetUserId();
            _userService = new UserService(_userId);
            var user = _userService.DetailedUser(_userId);
                return View(user);
        }
    }
}