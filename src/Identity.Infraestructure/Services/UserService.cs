using System.Linq;
using System.Threading.Tasks;
using Identity.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure.Services
{
	internal class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager) =>
			_userManager = userManager;

		public async Task<Result> CreateUser(ApplicationUser newUser, string password, string confirmPassword)
		{
			if (password != confirmPassword)
				return Result.Failure("The password does not match.");

			if (newUser.IsInvalid)
				return Result.Failure(newUser.Notifications);

			var result = await _userManager.CreateAsync(newUser, password);

			if (result.Succeeded)
				return Result.Success();

			return Result.Failure(result.Errors.Select(s => s.Description));
		}

		public async Task<Result> Register(ApplicationUser newUser, string password, string confirmPassword)
		{
			if (password != confirmPassword)
				return Result.Failure("The password does not match.");

			if (newUser.IsInvalid)
				return Result.Failure(newUser.Notifications);

			var result = await _userManager.CreateAsync(newUser, password);

			if (result.Succeeded)
				return Result.Success();

			return Result.Failure(result.Errors.Select(s => s.Description));
		}
	}
}
