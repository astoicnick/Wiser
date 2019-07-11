using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wiser.Services;

namespace Wiser.MVC.Controllers
{
    [RoutePrefix("api")]
    [Authorize]
    public class DynamicController : ApiController
    {

        private WisdomService _wisdomService;
        private UserService _userService;
        private string _userId;
        [HttpPut]
        [Route("Upvote/{id}")]
        public bool Upvote(int id)
        {
            _userId = User.Identity.GetUserId();
            _userService = new UserService(_userId);
            _wisdomService = new WisdomService(_userId);
            if (_userService.Upvote(id))
            {
                return true;
            }
            return false;
        }
        [HttpPost]
        [Route("Favorite/{id}")]
        public bool Favorite(int id)
        {
            _userId = User.Identity.GetUserId();
            var userService = new UserService(_userId);
            if (userService.AddFavorite(id, _userId))
            {
                return true;
            }
            return false;
        }
        [HttpDelete]
        [Route("rmfavorite/{id}")]
        public bool RemoveFavorite(int id)
        {
            _userId = User.Identity.GetUserId();
            _userService = new UserService(_userId);
            if (_userService.RemoveFavorite(id))
            {
                return true;
            }
            return false;
        }
    }
}
