using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using webapi.BookOperations.GetBookDetail;
using webapi.BookOperations.GetBooks;
using static webapi.BookOperations.CreateBook.CreateBookCommand;

namespace webapi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BookDetailViewModel>()
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())
                )
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                );
            CreateMap<Book, BooksViewModel>()
                .ForMember(
                    dest => dest.Genre,
                    opt => opt.MapFrom(src => ((GenreEnum)src.GenreId).ToString())
                )
                .ForMember(
                    dest => dest.PublishDate,
                    opt => opt.MapFrom(src => src.PublishDate.Date.ToString("dd/MM/yyyy"))
                );
        }
    }
}
