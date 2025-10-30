using AutoMapper;
using WeatherApp.Models.DTOs;

namespace WeatherApp.Services.MappingProfiles
{
    public class WeatherProfile : Profile
    {
        public WeatherProfile() 
        {
            CreateMap<WeatherResponseDto, WeatherDto>()
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Weather[0].Description))
                .ForMember(dest => dest.Summary, opt => opt.MapFrom(src => src.Weather[0].Main))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.Humidity, opt => opt.MapFrom(src => src.Main.Humidity))
                .ForMember(dest => dest.Temperature, opt => opt.MapFrom(src => src.Main.Temp))
                .ForMember(dest => dest.Wind, opt => opt.MapFrom(src => src.Wind));

            CreateMap<WindInfo, WindMetrics>()
                .ForMember(dest => dest.Direction, opt => opt.MapFrom(src => src.Deg));
        }
    }
}
