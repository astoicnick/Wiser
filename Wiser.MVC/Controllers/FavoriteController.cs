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
        //GET: Favorite/Author
        public ActionResult Author()
        {
            _userId = User.Identity.GetUserId();
            _userService = new UserService(_userId);
            var user = _userService.DetailedUser(_userId);
            var favScrolls = user.Favorites.Select(f => f.ScrollAuthor).ToList();
            var authorList = new AuthorService().GetDetailAuthors();
            var favAuthors = new List<Wiser.Models.Author.AuthorDetailItem>();
            //foreach (var author in authorList)
            //{
            //    if (favScrolls.Select(f=>f.AuthorId).Contains(author.AuthorId))
            //    {
            //        favAuthors.Add(author);
            //    }
            //}
            //foreach (var item in favScrolls)
            //{
            //    if item.AuthorId == 
            //}
            var attributions = new AuthorService().GetDetailAuthors().Where(a=>favScrolls.Select(f=>f.AuthorId).Contains(a.AuthorId));
            return View(attributions);
        }
    }
}