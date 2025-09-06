using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Services;

namespace Backend.Mappers
{
	public class MovieMapperProfile : Profile
	{
		public MovieMapperProfile()
		{
			CreateMap<Movie, MovieSummaryDto>()
				.ForMember(dest => dest.Image,
					// Take the first card image for movie summary
					opt => opt.MapFrom(src => src.CardImages != null && src.CardImages.Count > 0
						? new ImageDto
						{
							Hash = HashService.ComputeHash(src.CardImages[0].Url),
							Height = src.CardImages[0].Height,
							Width = src.CardImages[0].Width,
						}
						: new ImageDto()));

			CreateMap<Movie, MovieDetailDto>()
				.ForMember(dest => dest.CardImages,
					opt => opt.MapFrom(src => src.CardImages
						.Select(img => new ImageDto
						{
							Hash = HashService.ComputeHash(img.Url),
							Height = img.Height,
							Width = img.Width,
						}).ToList()))
				.ForMember(dest => dest.KeyArtImages,
					opt => opt.MapFrom(src => src.KeyArtImages
						.Select(img => new ImageDto
						{
							Hash = HashService.ComputeHash(img.Url),
							Height = img.Height,
							Width = img.Width,
						}).ToList()))
				.ForMember(dest => dest.Cast,
					opt => opt.MapFrom(src => src.Cast.Select(c => new PersonDto { Name = c.Name }).ToList()))
				.ForMember(dest => dest.Directors,
					opt => opt.MapFrom(src => src.Directors.Select(d => new PersonDto { Name = d.Name }).ToList()))
				.ForMember(dest => dest.Videos,
					opt => opt.MapFrom(src => src.Videos.Select(v => new VideoDto
					{
						Title = v.Title,
						Type = v.Type,
						Url = v.Url,
						Alternatives = v.Alternatives.Select(a => new VideoAlternativeDto
						{
							Quality = a.Quality,
							Url = a.Url
						}).ToList()
					}).ToList()))
				.ForMember(dest => dest.ViewingWindow,
					opt => opt.MapFrom(src => src.ViewingWindow != null
						? new ViewingWindowDto
						{
							StartDate = src.ViewingWindow.StartDate,
							EndDate = src.ViewingWindow.EndDate,
							WayToWatch = src.ViewingWindow.WayToWatch,
						}
						: null));
		}
	}
}
