using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webapi.DBOperations;

namespace webapi.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _dbContext;

        public UpdateGenreCommand(BookStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Handle()
        {
            var genre = _dbContext.Genres.SingleOrDefault(x => x.Id == GenreId);
            if (genre is null)
                throw new InvalidOperationException("Genre is not found!");
            if (
                _dbContext.Genres.Any(
                    x => x.Name.ToLower() == Model.Name.ToLower() && x.Id != GenreId
                )
            )
                throw new InvalidOperationException("Genre is already exist!");
            genre.Name = string.IsNullOrEmpty(Model.Name.Trim()) ? genre.Name : Model.Name;
            genre.isActive = Model.IsActive;
            _dbContext.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        public string Name { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
