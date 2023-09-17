using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using webapi.Services;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            // Get a list of books
            var books = _bookService.GetBooks();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Get a book by its ID
            var book = _bookService.GetById(id);
            if (book == null)
                return NotFound("Book not found");

            return Ok(book);
        }

        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            if (newBook == null)
                return BadRequest("Invalid data");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Add a new book to the database
            _bookService.AddBook(newBook);

            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            if (updatedBook == null)
                return BadRequest("Invalid data");

            var book = _bookService.GetById(id);
            if (book == null)
                return NotFound("Book not found");

            // Update an existing book
            _bookService.UpdateBook(id, updatedBook);

            return Ok(book);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var book = _bookService.GetById(id);
            if (book == null)
                return NotFound("Book not found");

            // Delete a book by its ID
            _bookService.DeleteBook(id);

            return Ok(book);
        }

        [HttpGet("list")]
        public IActionResult ListBooks(
            [FromQuery] string? name = null,
            [FromQuery] string sortBy = "Id"
        )
        {
            var bookList = _bookService.FilterAndSortBooks(name, sortBy);

            return Ok(bookList);
        }
    }
}
