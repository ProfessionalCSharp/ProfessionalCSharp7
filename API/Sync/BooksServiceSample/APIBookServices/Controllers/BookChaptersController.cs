using BooksServiceSample.Models;
using BooksServiceSample.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BooksServiceSample.Controllers
{
    [Produces("application/json", "application/xml")]
    [Route("api/[controller]")]
    public class BookChaptersController : Controller
    {
        private readonly IBookChaptersService _bookChaptersService;
        public BookChaptersController(IBookChaptersService bookChaptersService)
        {
            _bookChaptersService = bookChaptersService;
        }

        // GET api/bookchapters
        [HttpGet]
        public IEnumerable<BookChapter> GetBookChapters() => _bookChaptersService.GetAll();

        // GET api/bookchapters/guid
        [HttpGet("{id}", Name = nameof(GetBookChapterById))]
        public IActionResult GetBookChapterById(Guid id)
        {
            BookChapter chapter = _bookChaptersService.Find(id);
            if (chapter == null)
            {
                return NotFound();
            }
            else
            {
                return new ObjectResult(chapter);
            }
        }

        // POST api/bookchapters
        [HttpPost]
        public IActionResult PostBookChapter([FromBody]BookChapter chapter)
        {
            if (chapter == null)
            {
                return BadRequest();
            }
            _bookChaptersService.Add(chapter);
            return CreatedAtRoute(nameof(GetBookChapterById), new { id = chapter.Id }, chapter);
        }

        // PUT api/bookchapters/guid
        [HttpPut("{id}")]
        public IActionResult PutBookChapter(Guid id, [FromBody]BookChapter chapter)
        {
            if (chapter == null || id != chapter.Id)
            {
                return BadRequest();
            }
            if (_bookChaptersService.Find(id) == null)
            {
                return NotFound();
            }
            _bookChaptersService.Update(chapter);
            return new NoContentResult();
        }

        // DELETE api/bookchapters/5
        [HttpDelete("{id}")]
        public void Delete(Guid id) =>
            _bookChaptersService.Remove(id);
    }
}
