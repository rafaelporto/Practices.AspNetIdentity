using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infrastructure.Entities;

namespace Identity.Infrastructure
{
	public interface IUserService
	{
		Task<Result<ApplicationUser, IEnumerable<string>>> Register(ApplicationUser newUser, string password, string confirmedPassword);
	}
}
