using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.RequestFeatures;

namespace MovieStoreAppWebAPI.Extensions.Repository
{
    public static class OrderRepositoryExtensions
    {
        public static IQueryable<Order> FilterByFilmName(this IQueryable<Order> orders,
    OrderBaseParameters parameters)
        {
            if (parameters.FilmName != null)
                return orders.Where(x => x.Film.Name.ToLower().Contains(parameters.FilmName.ToLower()));
            return orders;
        }

        public static IQueryable<Order> FilterByUserId(this IQueryable<Order> orders,
            int id)
        {
            if (id != 0)
                return orders.Where(x => x.User.Id == id);
            return orders;
        }

        public static IQueryable<Order> FilterByOrderedDate(this IQueryable<Order> orders,
            OrderBaseParameters parameters)
        {
            if (parameters.MinimumOrderedDate != null && parameters.MaximumOrderedDate == null)
                return orders.Where(x => x.OrderedDate > parameters.MinimumOrderedDate);
            if (parameters.MinimumOrderedDate == null && parameters.MaximumOrderedDate != null)
                return orders.Where(x => x.OrderedDate < parameters.MaximumOrderedDate);
            if (parameters.MinimumOrderedDate != null && parameters.MaximumOrderedDate != null)
                return orders.Where(x => x.OrderedDate > parameters.MinimumOrderedDate &&
                    x.OrderedDate < parameters.MaximumOrderedDate);
            return orders;
        }
    }
}