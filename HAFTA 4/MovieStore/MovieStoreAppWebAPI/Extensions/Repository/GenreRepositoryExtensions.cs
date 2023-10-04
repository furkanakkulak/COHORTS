using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class GenreRepositoryExtensions
    {
        public static IQueryable<Genre> FilterByName(this IQueryable<Genre> entities,
            RequestParameters parameters
        )
        {
            if (parameters.Name != null)
                return entities.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            else
                return entities;
        }
    }
}
