using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace webapi.Services
{
    public class BookService : IBookService
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book
            {
                Id = 1,
                Title = "Lean Startup",
                GenreId = 1, // Example genre: Personal Growth
                PageCount = 200,
                PublishDate = new DateTime(2001, 01, 01)
            },
            new Book
            {
                Id = 2,
                Title = "Herland",
                GenreId = 2, // Example genre: Science Fiction
                PageCount = 250,
                PublishDate = new DateTime(2010, 05, 23)
            },
            new Book
            {
                Id = 3,
                Title = "Dune",
                GenreId = 2, // Example genre: Science Fiction
                PageCount = 540,
                PublishDate = new DateTime(2005, 01, 01)
            },
        };

        public List<Book> GetBooks()
        {
            return BookList;
        }

        public Book GetById(int id)
        {
            return BookList.FirstOrDefault(book => book.Id == id);
        }

        public void AddBook(Book newBook)
        {
            // Generate a new ID for the new book
            newBook.Id = BookList.Max(b => b.Id) + 1;

            // Add the new book to the list
            BookList.Add(newBook);
        }

        public void UpdateBook(int id, Book updatedBook)
        {
            // Find the book to be updated
            var book = BookList.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                // Update the book properties
                book.Title = updatedBook.Title;
                book.GenreId = updatedBook.GenreId;
                book.PageCount = updatedBook.PageCount;
                book.PublishDate = updatedBook.PublishDate;
            }
        }

        public void DeleteBook(int id)
        {
            // Find the book to be deleted
            var book = BookList.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                // Remove the book from the list
                BookList.Remove(book);
            }
        }

        public List<Book> FilterAndSortBooks(string name, string sortBy)
        {
            IQueryable<Book> query = BookList.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(
                    book => book.Title.Contains(name, StringComparison.OrdinalIgnoreCase)
                );
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

            return query.ToList();
        }
    }
}
