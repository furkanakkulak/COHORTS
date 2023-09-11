using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]s")]
    public class BookController : ControllerBase
    {
         private static List<Book> BookList = new List<Book>(){
            new Book{
                Id=1,
                Title="Lean Startup",
                GenreId=1, // Personal Growth
                PageCount=200,
                PublishDate=new DateTime(2001, 01, 01)
            },
            new Book{
                Id=2,
                Title="Herland",
                GenreId=2, // Science Fiction
                PageCount=250,
                PublishDate=new DateTime(2010, 05, 23)
            },
            new Book{
                Id=3,
                Title="Dune",
                GenreId=2, // Science Fiction
                PageCount=540,
                PublishDate=new DateTime(2005, 01, 01)
            },
            
         };

        [HttpGet]
         public IActionResult GetBooks([FromQuery] string name = null, [FromQuery] string sortBy = "Id")
        {
            IQueryable<Book> query = BookList.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(book => book.Title.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            switch (sortBy.ToLower())
            {
                case "title":
                    query = query.OrderBy(book => book.Title);
                    break;
                case "pagecount":
                    query = query.OrderBy(book => book.PageCount);
                    break;
                case "publishdate":
                    query = query.OrderBy(book => book.PublishDate);
                    break;
                default:
                    query = query.OrderBy(book => book.Id);
                    break;
            }

            var bookList = query.ToList();
            return Ok(bookList);
        }

          [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            if (book is null)
                return NotFound("Book not found");
            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook) {
            var book = BookList.SingleOrDefault(book => book.Title == newBook.Title);
            if(book is not null)
                return BadRequest("Book already exists");
            
            BookList.Add(newBook);
            return Ok(newBook);
        }
        

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook) {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            if(book is null)
                return NotFound("Book not found");

            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount = updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate = updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            return Ok(book);
        }
        
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = BookList.SingleOrDefault(book => book.Id == id);
            if (book == null)
                return NotFound("Book not found");

            BookList.Remove(book);
            return NoContent();
        }
    }
}