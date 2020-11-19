using System.Linq;
using System.Threading.Tasks;
using Identity.Infraestructure.Entities;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure.Services
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		public AuthService(UserManager<User> userManager, SignInManager<User> signInManager) =>
			(_userManager, _signInManager) = (userManager, signInManager);

		public async Task<Result<bool>> Login(string email, string password)
		{
			if (email is null or "")
				return Result.Failure<bool>("O nome do usuário é obrigatório");
			
			if (password is null or "")
				return Result.Failure<bool>("O password do usuário é obrigatório");

			var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

			if (result.Succeeded)
				return Result.Success(true);

			return Result.Failure<bool>("Não foi possível fazer o login");
		}

		public async Task<Result> Register(User newUser, string password, string confirmPassword)
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

		//TODO: Implementar método de confirmação de conta
	}
}
