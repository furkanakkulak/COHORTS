using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class UserRepositoryExtensions
    {
        public static IQueryable<User> FilterByEmail(this IQueryable<User> entities,
        UserParameters parameters
        )
        {
            if (parameters.Email != null)
                return entities.Where(x => x.Email.ToLower().Contains(parameters.Email.ToLower()));
            else
                return entities;
        }

        public static IQueryable<User> FilterByUsername(this IQueryable<User> entities,
        UserParameters parameters
        )
        {
            if (parameters.Username != null)
                return entities.Where(x => x.Username.ToLower().Contains(parameters.Username.ToLower()));
            else
                return entities;
        }

        public static IQueryable<User> FilterByName(this IQueryable<User> entities,
        RequestParameters parameters
        )
        {
            if (parameters.Name != null)
                return entities.Where(x => x.Name.ToLower().Contains(parameters.Name.ToLower()));
            else
                return entities;
        }

        public static IQueryable<User> FilterByNameSurname(this IQueryable<User> users,
            UserParameters parameters
        )
        {
            if (parameters.Name != null && parameters.Surname == null)
                return users.FilterByName(parameters);
            if (parameters.Name == null && parameters.Surname != null)
                return users.Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            if (parameters.Name != null && parameters.Surname != null)
                return users.FilterByName(parameters).Where(x => x.Surname.ToLower().Contains(parameters.Surname.ToLower()));
            return users;
        }
    }
}
