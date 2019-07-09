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
    public class DynamicController : ApiController
    {

        private WisdomService _wisdomService;
        [HttpPut]
        [Route("Upvote/{id}")]
        public bool Upvote(int id)
        {
            var userId = User.Identity.GetUserId();
            var userService = new UserService(userId);
            _wisdomService = new WisdomService(userId);
            if (userService.Upvote(id))
            {
                return true;
            }
            return false;

        }
    }
}
