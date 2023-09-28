using System;
using System.Linq;
using webapi.DBOperations;

namespace webapi.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _dbContext;
        public int AuthorId { get; set; }

        public DeleteAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Author not found!");

            var hasBooks = _dbContext.Books.Any(x => x.Id == AuthorId);
            if (hasBooks)
                throw new InvalidOperationException("Author has books, cannot be deleted!");

            _dbContext.Authors.Remove(author);
            _dbContext.SaveChanges();
        }
    }
}
