using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class FilmRepositoryExtensions
    {
        public static IQueryable<Film> FilterByGenreId(this IQueryable<Film> films,
    FilmParameters parameters
)
        {
            if (parameters.GenreId == 0)
                return films;
            return films.Where(x => x.Genre.Id == parameters.GenreId);
        }

        public static IQueryable<Film> FilterByDirectorId(this IQueryable<Film> films,
            FilmParameters parameters
        )
        {
            if (parameters.DirectorId == 0)
                return films;
            return films.Where(x => x.Director.Id == parameters.DirectorId);
        }

        public static IQueryable<Film> FilterByPlayerIds(this IQueryable<Film> films,
            FilmParameters parameters
        )
        {
            if (parameters.PlayerIds.Count == 0)
                return films;

            return films.Where(x => x.Players.SingleOrDefault(p => parameters.PlayerIds.Contains(p.Id)) != null);
        }

        public static IQueryable<Film> FilterByName(this IQueryable<Film> entities,
            RequestParameters parameters
        )
        {
            if (parameters.Name != null)
                return entities.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            else
                return entities;
        }

        public static IQueryable<Film> FilterByPublishedDate(this IQueryable<Film> entities,
            FilmParameters parameters)
        {
            if (parameters.MinimumPublishedDate != null && parameters.MaximumPublishedDate == null)
                return entities.Where(x => x.PublishedDate > parameters.MinimumPublishedDate);
            if (parameters.MinimumPublishedDate == null && parameters.MaximumPublishedDate != null)
                return entities.Where(x => x.PublishedDate < parameters.MaximumPublishedDate);
            if (parameters.MinimumPublishedDate != null && parameters.MaximumPublishedDate != null)
                return entities.Where(x => x.PublishedDate > parameters.MinimumPublishedDate &&
                    x.PublishedDate < parameters.MaximumPublishedDate);
            return entities;
        }


    }
}