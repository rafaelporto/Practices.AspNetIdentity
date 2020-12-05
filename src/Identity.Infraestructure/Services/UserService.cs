using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure
{
	internal class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public UserService(UserManager<ApplicationUser> userManager) =>
			_userManager = userManager;

		public async Task<Result<ApplicationUser, IEnumerable<string>>> Register(ApplicationUser newUser,
					string password, string confirmPassword)
		{
			if (password != confirmPassword)
				return Result.Failure<ApplicationUser, IEnumerable<string>>(new string[] { "The password does not match." });

			if (newUser.IsInvalid)
				return Result.Failure<ApplicationUser, IEnumerable<string>>(newUser.Notifications);

			var result = await _userManager.CreateAsync(newUser, password);

			if (result.Succeeded)
				return Result.Success<ApplicationUser, IEnumerable<string>>(newUser);

			return Result.Failure<ApplicationUser, IEnumerable<string>>(result.Errors.Select(s => s.Description));
		}
	}
}
