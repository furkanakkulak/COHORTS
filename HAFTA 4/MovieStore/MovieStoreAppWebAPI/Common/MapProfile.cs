using AutoMapper;

using MovieStoreAppWebAPI.Entities;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Create;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Read;
using MovieStoreAppWebAPI.Operations.DirectorOperation.Update;
using MovieStoreAppWebAPI.Operations.FilmOperation.Create;
using MovieStoreAppWebAPI.Operations.FilmOperation.Read;
using MovieStoreAppWebAPI.Operations.FilmOperation.Update;
using MovieStoreAppWebAPI.Operations.GenreOperation.Create;
using MovieStoreAppWebAPI.Operations.GenreOperation.Read;
using MovieStoreAppWebAPI.Operations.OrderOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Create;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Read;
using MovieStoreAppWebAPI.Operations.PlayerOperation.Update;
using MovieStoreAppWebAPI.Operations.UserOperation.Create;
using MovieStoreAppWebAPI.Operations.UserOperation.Read;

namespace MovieStoreAppWebAPI.Common;

public class MapProfile : Profile
{
    public MapProfile()
    {
        CreateMap<Film, ReadFilmViewModel>();
        CreateMap<CreateFilmViewModel, Film>();
        CreateMap<UpdateFilmViewModel, Film>();

        CreateMap<Player, ReadPlayerViewModel>();
        CreateMap<CreatePlayerViewModel, Player>();
        CreateMap<UpdatePlayerViewModel, Player>();

        CreateMap<Genre, ReadGenreViewModel>();
        CreateMap<CreateGenreViewModel, Genre>();
        CreateMap<UpdateFilmViewModel, Film>();

        CreateMap<Director, ReadDirectorViewModel>();
        CreateMap<CreateDirectorViewModel, Director>();
        CreateMap<UpdateDirectorViewModel, Director>();

        CreateMap<User, ReadUserViewModel>();
        CreateMap<CreateUserViewModel, User>();

        CreateMap<Order, ReadOrderViewModel>();

        


    }
}