using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class DirectorRepositoryExtensions
    {
        public static IQueryable<Director> FilterByName(this IQueryable<Director> entities,
    RequestParameters parameters
)
        {
            if (parameters.Name != null)
                return entities.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            else
                return entities;
        }

        public static IQueryable<Director> FilterByNameSurname(this IQueryable<Director> directors,
    DirectorParameters parameters
)
        {
            if (parameters.Name != null && parameters.Surname == null)
                return directors.FilterByName(parameters);
            if (parameters.Name == null && parameters.Surname != null)
                return directors.Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            if (parameters.Name != null && parameters.Surname != null)
                return directors.FilterByName(parameters).Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            return directors;
        }
    }
}