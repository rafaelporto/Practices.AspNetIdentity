using AutoMapper;
using Identity.Api.AuthEndpoints;
using Identity.Infraestructure.Entities;

namespace Identity.Api.Mapper
{
	public class UserProfile : Profile
	{
		public UserProfile() =>
			CreateMap<RegisterUserRequest, ApplicationUser>()
			.ConstructUsing(input => ApplicationUser.NewUser(input.Name, input.LastName, input.Email));
	}
}
