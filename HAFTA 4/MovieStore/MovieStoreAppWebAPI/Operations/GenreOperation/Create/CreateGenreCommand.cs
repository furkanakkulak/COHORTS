using AutoMapper;

using MovieStoreAppWebAPI.Constants;
using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DatabaseOperation;
using MovieStoreAppWebAPI.Utilities.Results;

using System;
using System.Collections.Generic;
using System.Linq;

namespace MovieStoreAppWebAPI.Operations.GenreOperation.Create
{
    public class CreateGenreCommand
    {
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateGenreViewModel Model { get; set; } = new CreateGenreViewModel();
        public CreateGenreCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Result Handle()
        {
            CheckIfGenreAlreadyExists();

            Genre newGenre = _mapper.Map<Genre>(Model);

            _dbContext.Genres.Add(newGenre);

            _dbContext.SaveChanges();

            return new SuccessResult(message: Messages.GenreSuccessfullyCreated);
        }


        private void CheckIfGenreAlreadyExists()
        {
            Genre? searchedGenre = _dbContext.Genres.SingleOrDefault(x => x.Name == Model.Name);

            if (searchedGenre != null)
                throw new InvalidOperationException(Messages.GenreAlreadyExists);
        }

    }

    public class CreateGenreViewModel
    {
        public string? Name { get; set; }
    }
}
