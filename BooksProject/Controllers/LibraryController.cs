using BooksProject.Context;
using BooksProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BooksProject.Controllers
{
    [Route("api")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private AppDbContext _appDbContext;

        public LibraryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var res = _appDbContext.Books.Select(x => new
            {
                Id = x.Id,
                Name = x.Name,
                Author = x.Author,
                Year = x.Year,
                Genre = x.Genre,
                Price = x.Price,
                Rating = x.Rating,
                Reviews = x.Reviews,
            });

            return Ok(res);
        }

        //[HttpPost("Post")]
        //public async Task<ActionResult> Create([FromBody] Book model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _appDbContext.Books.Add(model);
        //    await _appDbContext.SaveChangesAsync();

        //    return CreatedAtAction("Get", new { id = model.Id }, model);
        //}


        [HttpPost("PostBook")]
        public IActionResult PostBook(Book item)
        {
            var res = _appDbContext.Books.Add(item);
            return Ok(res);
        }
 

        [HttpDelete("DeleteBook")]

        public IActionResult DeleteBook(int id)
        {
            var res = _appDbContext.Books.First(x => x.Id == id);
            _appDbContext.Books.Remove(res);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Book model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != model.Id)
            {
                return BadRequest();
            }

            _appDbContext.Entry(model).State = EntityState.Modified;

            try
            {
                await _appDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        private bool ModelExists(int id)
        {
            return _appDbContext.Books.Any(e => e.Id == id);
        }




    }
}
