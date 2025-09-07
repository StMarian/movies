using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Utils;

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

			CreateMap<Person, PersonDto>()
				.ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.SanitizeText()));
			CreateMap<Video, VideoDto>();
			CreateMap<ViewingWindow, ViewingWindowDto>();
			CreateMap<Video, VideoDto>();
			CreateMap<VideoAlternative, VideoAlternativeDto>();

			CreateMap<Movie, MovieSummaryDto>()
				.ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.Headline.SanitizeText()));

			CreateMap<Movie, MovieDetailDto>()
				.ForMember(dest => dest.Body, opt => opt.MapFrom(src => src.Body.SanitizeText()))
				.ForMember(dest => dest.Synopsis, opt => opt.MapFrom(src => src.Synopsis.SanitizeText()))
				.ForMember(dest => dest.Headline, opt => opt.MapFrom(src => src.Headline.SanitizeText()))
				.ForMember(dest => dest.Quote, opt => opt.MapFrom(src => src.Quote.SanitizeText()));
		}
	}
}
