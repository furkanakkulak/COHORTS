using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class PlayerRepositoryExtensions
    {
        public static IQueryable<Player> FilterByName(this IQueryable<Player> entities,
    RequestParameters parameters
)
        {
            if (parameters.Name != null)
                return entities.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            else
                return entities;
        }

        public static IQueryable<Player> FilterByNameSurname(this IQueryable<Player> players,
            PlayerParameters parameters
        )
        {
            if (parameters.Name != null && parameters.Surname == null)
                return players.FilterByName(parameters);
            if (parameters.Name == null && parameters.Surname != null)
                return players.Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            if (parameters.Name != null && parameters.Surname != null)
                return players.FilterByName(parameters).Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            return players;
        }
    }
}