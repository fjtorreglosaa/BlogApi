using AutoMapper;
using BlogApi.Application.Utilities.DTOs.BlogDTOs;
using BlogApi.Domain.Models;

namespace BlogApi.Application.Utilities
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Blog, BlogDTO>()
                .ForMember(x => x.BlogName, opt => opt.MapFrom(src => src.Title))
                .ReverseMap();
        }
    }
}
