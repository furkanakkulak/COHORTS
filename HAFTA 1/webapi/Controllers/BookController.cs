using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        // A static list to hold the book data.
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, // Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001, 01, 01)
            },
            new Book
            {
                Id = 2,
                Title = "Herland",
                GenreId = 2, // Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2005, 01, 01)
            },
        };

        // GET: Retrieve all books.
        [HttpGet]
        public IActionResult GetBooks()
        {
            // Order the book list by ID and convert it to a list.
            var bookList = BookList.OrderBy(book => book.Id).ToList();

            // Return the sorted book list.
            return Ok(bookList);
        }

        // GET: Retrieve a specific book by ID.
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            // Find the book with the specified ID in the list.
            var book = BookList.SingleOrDefault(book => book.Id == id);

            // Check if the book exists.
            if (book == null)
                return NotFound("Book not found");

            // Return the found book.
            return Ok(book);
        }

        // POST: Add a new book.
        [HttpPost]
        public IActionResult AddBook([FromBody] Book newBook)
        {
            // Check if the received data is null.
            if (newBook == null)
            {
                return BadRequest("Invalid data");
            }

            // Check if the model state is valid.
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if a book with the same title already exists.
            var existingBook = BookList.FirstOrDefault(book => book.Title == newBook.Title);
            if (existingBook != null)
            {
                return Conflict("Book already exists");
            }

            // Generate a new ID for the book.
            newBook.Id = BookList.Max(b => b.Id) + 1;

            // Add the new book to the list.
            BookList.Add(newBook);

            // Return a 201 Created response with the newly created book.
            return CreatedAtAction(nameof(GetById), new { id = newBook.Id }, newBook);
        }

        // PUT: Update an existing book by ID.
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] Book updatedBook)
        {
            // Check if the received data is null.
            if (updatedBook == null)
                return BadRequest("Invalid data");

            // Find the book with the specified ID in the list.
            var book = BookList.FirstOrDefault(b => b.Id == id);

            // Check if the book exists.
            if (book == null)
                return NotFound("Book not found");

            // Update the book properties with non-default values from the updated book.
            book.Title = updatedBook.Title != default ? updatedBook.Title : book.Title;
            book.GenreId = updatedBook.GenreId != default ? updatedBook.GenreId : book.GenreId;
            book.PageCount =
                updatedBook.PageCount != default ? updatedBook.PageCount : book.PageCount;
            book.PublishDate =
                updatedBook.PublishDate != default ? updatedBook.PublishDate : book.PublishDate;

            // Return the updated book.
            return Ok(book);
        }

        // DELETE: Delete a book by ID.
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            // Find the book with the specified ID in the list.
            var book = BookList.FirstOrDefault(b => b.Id == id);

            // Check if the book exists.
            if (book == null)
                return NotFound("Book not found");

            // Remove the book from the list.
            BookList.Remove(book);

            // Return the deleted book.
            return Ok(book);
        }

        // GET: List and filter books with optional query parameters.
        [HttpGet("list")]
        public IActionResult ListBooks(
            [FromQuery] string name = null,
            [FromQuery] string sortBy = "Id"
        )
        {
            // Create an IQueryable<Book> from the book list.
            IQueryable<Book> query = BookList.AsQueryable();

            // Filter books by name if a name query parameter is provided.
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(
                    book => book.Title.Contains(name, StringComparison.OrdinalIgnoreCase)
                );
            }

            // Sort books based on the sortBy query parameter.
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

            // Convert the filtered and sorted query to a list.
            var bookList = query.ToList();

            // Return the filtered and sorted book list.
            return Ok(bookList);
        }
    }
}
