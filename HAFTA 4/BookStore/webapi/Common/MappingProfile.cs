using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using webapi.Applications.AuthorOperations.Queries.GetAuthor;
using webapi.Applications.AuthorOperations.Queries.GetAuthorDetail;
using webapi.Applications.BookOperations.Queries.GetBookDetail;
using webapi.Applications.BookOperations.Queries.GetBooks;
using webapi.Applications.GenreOperations.Queries.GetGenreDetail;
using webapi.Applications.GenreOperations.Queries.GetGenres;
using webapi.Entities;
using static webapi.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace webapi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                )
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName)
                );

            CreateMap<Book, BooksViewModel>()
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.Name))
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                )
                .ForMember(
                    dest => dest.Author,
                    opt => opt.MapFrom(src => src.Author.FirstName + " " + src.Author.LastName)
                );

            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();

            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<Author, AuthorsViewModel>();
        }
    }
}
