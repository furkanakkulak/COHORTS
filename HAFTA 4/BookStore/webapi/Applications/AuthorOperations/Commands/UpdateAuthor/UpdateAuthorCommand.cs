using System;
using System.Linq;
using webapi.DBOperations;

namespace webapi.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateAuthorCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(x => x.Id == AuthorId);
            if (author is null)
                throw new InvalidOperationException("Author not found!");

            if (
                _dbContext.Authors.Any(
                    x =>
                        x.FirstName.ToLower() == Model.FirstName.ToLower()
                        && x.LastName.ToLower() == Model.LastName.ToLower()
                        && x.Id != AuthorId
                )
            )
                throw new InvalidOperationException("Author with the same name already exists!");

            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.BirthDate = Model.BirthDate;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
