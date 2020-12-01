using System.Threading.Tasks;
using Identity.Infraestructure.Entities;

namespace Identity.Infraestructure.Services
{
	public interface IUserService
	{
		Task<Result> CreateUser(ApplicationUser newUser, string password, string confirmPassword);
		Task<Result> Register(ApplicationUser newUser, string password, string confirmedPassword);
	}
}
