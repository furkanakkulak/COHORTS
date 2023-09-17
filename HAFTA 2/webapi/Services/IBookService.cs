using System.Collections.Generic;

namespace webapi.Services
{
    public interface IBookService
    {
        List<Book> GetBooks();
        Book GetById(int id);
        void AddBook(Book newBook);
        void UpdateBook(int id, Book updatedBook);
        void DeleteBook(int id);

        List<Book> FilterAndSortBooks(string name, string sortBy);
    }
}
