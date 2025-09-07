using AutoMapper;
using Backend.Models;
using Backend.Models.DTO;
using Backend.Services;

namespace Backend.Mappers
{
	public class ImageHashResolver(ImageHashService hashService) : IValueResolver<Image, ImageDto, string>
	{
		public string Resolve(Image source, ImageDto destination, string destMember, ResolutionContext context)
		{
			return hashService.ComputeAndStoreHash(source.Url);
		}
	}
}
