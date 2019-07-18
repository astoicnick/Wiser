using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Wiser.Models.Author;
using Wiser.Services;

namespace Wiser.WebAPI.Controllers
{
    public class AuthorController : ApiController
    {
        private AuthorService _authorService;

        [HttpGet]
        public IHttpActionResult All()
        {
            _authorService = new AuthorService();
            var authors = _authorService.GetAuthors();
            return Ok(authors);
        }
        [HttpGet]
        public IHttpActionResult Detail(int id)
        {
            _authorService = new AuthorService();
            var author = _authorService.RetrieveAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            return Ok(author);
        }
        [HttpPost]
        public IHttpActionResult Create(AuthorCreateItem authorToCreate)
        {
            if (authorToCreate == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _authorService = new AuthorService();
            if (!_authorService.CreateAuthor(authorToCreate))
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpPut]
        public IHttpActionResult Update(AuthorUpdateItem authorToUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _authorService = new AuthorService();
            if (!_authorService.UpdateAuthor(authorToUpdate))
            {
                return InternalServerError();
            }
            return Ok();
        }
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _authorService = new AuthorService();
            if (!_authorService.DeleteAuthor(_authorService.DetailToUpdate(_authorService.RetrieveAuthor(id))))
            {
                return InternalServerError();
            }
            return Ok();
        }
    }
}
