using AutoMapper;
using BookCatalogApi.DTOs;
using BookCatalogApi.Entities;

namespace BookCatalogApi.Mappings;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>();
        CreateMap<BookCreateUpdateDto, Book>();
    }
}