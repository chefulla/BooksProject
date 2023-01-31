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
        public void Put(int id, [FromBody] string value)
        {
            var res = _appDbContext.Books.First(x => x.Id == id);
            _appDbContext.Books.Update(res);
           
        }
    }
}
