using AutoMapper;
using Identity.Api.AuthEndpoints;
using Identity.Infraestructure.Entities;

namespace Identity.Api.Mapper
{
	public class UserProfile : Profile
	{
		public UserProfile() =>
			CreateMap<RegisterUserRequest, User>();
	}
}
