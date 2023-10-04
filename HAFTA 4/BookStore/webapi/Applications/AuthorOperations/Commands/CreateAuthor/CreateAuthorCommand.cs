using System;
using System.Linq;
using AutoMapper;
using webapi.DBOperations;
using webapi.Entities;

namespace webapi.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _dbContext;

        private readonly IMapper _mapper;

        public CreateAuthorCommand(IBookStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public void Handle()
        {
            var author = _dbContext.Authors.SingleOrDefault(
                x =>
                    x.FirstName.ToLower() == Model.FirstName.ToLower()
                    && x.LastName.ToLower() == Model.LastName.ToLower()
            );
            if (author is not null)
                throw new InvalidOperationException("Author already exists!");
            author = new Author();
            author.FirstName = Model.FirstName;
            author.LastName = Model.LastName;
            author.BirthDate = Model.BirthDate;
            _dbContext.Authors.Add(author);
            _dbContext.SaveChanges();
        }
    }

    public class CreateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
