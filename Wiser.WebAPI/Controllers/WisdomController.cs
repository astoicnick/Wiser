using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wiser.Models.Wisdom;
using Wiser.Services;

namespace Wiser.WebAPI.Controllers
{
    [Authorize]
    public class WisdomController : ApiController
    {
        private WisdomService _wisdomService;

        private WisdomService CreateWisdomService()
        {
            _wisdomService = new WisdomService(User.Identity.GetUserId());
            return _wisdomService;
        }
        [HttpGet]
        public IHttpActionResult All()
        {
            _wisdomService = CreateWisdomService();
            var wisdom = _wisdomService.GetWisdomList();
            return Ok(wisdom);
        }
        [HttpGet]
        public IHttpActionResult Detail(int id)
        {
            _wisdomService = CreateWisdomService();
            var wisdom = _wisdomService.RetrieveWisdomById(id);
            if (wisdom == null)
            {
                return NotFound();
            }
            return Ok(wisdom);
        }
        [HttpPost]
        public IHttpActionResult Create(WisdomCreateItem wisdomToCreate)
        {
            if (wisdomToCreate == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _wisdomService = CreateWisdomService();
            if (!_wisdomService.CreateWisdom(wisdomToCreate))
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Update(WisdomUpdateItem wisdomToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _wisdomService = CreateWisdomService();
            if (!_wisdomService.UpdateWisdom(wisdomToUpdate))
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _wisdomService = CreateWisdomService();
            if (!_wisdomService.RemoveWisdom(id))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
