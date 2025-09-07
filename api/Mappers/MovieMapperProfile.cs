using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;

namespace Backend.Mappers
{
	public class MovieMapperProfile : Profile
	{
		public MovieMapperProfile()
		{
			CreateMap<Image, ImageDto>()
				.ForMember(dest => dest.Hash, opt => opt.MapFrom<ImageHashResolver>())
				.ForMember(dest => dest.Height, opt => opt.MapFrom(src => src.Height))
				.ForMember(dest => dest.Width, opt => opt.MapFrom(src => src.Width));

			CreateMap<Person, PersonDto>();
			CreateMap<Video, VideoDto>();
			CreateMap<ViewingWindow, ViewingWindowDto>();
			CreateMap<Video, VideoDto>();
			CreateMap<VideoAlternative, VideoAlternativeDto>();

			CreateMap<Movie, MovieSummaryDto>();
			CreateMap<Movie, MovieDetailDto>();
		}
	}
}
