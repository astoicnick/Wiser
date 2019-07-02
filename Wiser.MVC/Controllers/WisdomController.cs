using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Wiser.Contracts;
using Wiser.Data;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [Authorize]
    public class WisdomController : Controller
    {
        private WisdomService _wisdomService;
        private string _userId;
        //Infinity Scroll
        // GET: Wisdom
        public ActionResult Index()
        {
            _userId = User.Identity.GetUserId();
            _wisdomService = new WisdomService(_userId);
            return View(_wisdomService.GetWisdomList());
        }
    }
}