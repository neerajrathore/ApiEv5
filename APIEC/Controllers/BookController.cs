using System.Collections.Generic;
using System.Linq;
using APIEC.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        static private List<Book> books = new List<Book>
        {
            new Book
            {
                Id = 1,
            },
            new Book
            {
                Id = 2,
            }
        };

        [HttpGet]
        public ActionResult<List<Book>> GetBooks()
        {
            return Ok(books);
        }

        [HttpGet("{id}")]
        public ActionResult<Book> GetBookById(int id)
        {
            var book = books.Find(a => a.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            else return book;
        }

        [HttpPost("add-book")]
        public ActionResult<Book> AddNewBook(Book newBook)
        {
            if (newBook == null)
            {
                return BadRequest();
            }
            else
            {
                books.Add(newBook);
                return CreatedAtAction(nameof(GetBookById), new { id = newBook.Id }, newBook);
            }
        }
    }
}
