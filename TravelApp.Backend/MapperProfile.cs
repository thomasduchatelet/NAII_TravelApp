using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelApp.Backend.Models;
using TravelApp.Shared.Dto;

namespace TravelApp.Backend
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<Trip, TripDto>().ReverseMap();

            CreateMap<Trip, GetTripDto>()
                .ForMember(dst => dst.Items, opt => opt.MapFrom(src => src.Items))
                .ForMember(dst => dst.Todos, opt => opt.MapFrom(src => src.Todos))
                .ForMember(dst => dst.Itinerary, opt => opt.MapFrom(src => src.Itinerary))
                .ReverseMap();

            CreateMap<Todo, TodoDto>()
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<Item, ItemDto>()
                .ForMember(dst => dst.Category, opt => opt.MapFrom(src => src.Category))
                .ReverseMap();

            CreateMap<Itinerary, ItineraryDto>()
                .ForMember(dst => dst.Locations, opt => opt.MapFrom(src => src.Locations))
                .ReverseMap();

            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Location, LocationDto>().ReverseMap();
        }
    }

}
