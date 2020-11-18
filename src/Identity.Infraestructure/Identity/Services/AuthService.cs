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

		//TODO: Implementar método de login
		public async Task<Result<bool>> Login(string email, string password)
		{
			if (email is null or "")
				return Result<bool>.Fail("O nome do usuário é obrigatório");
			
			if (password is null or "")
				return Result<bool>.Fail("O password do usuário é obrigatório");

			var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

			if (result.Succeeded)
				return Result<bool>.Ok(true);

			return Result<bool>.Fail("Não foi possível fazer o login");
		}

		public Task<Result<bool>> Register(User newUser, string password)
		{
			return Task.FromResult(Result<bool>.Ok(true));
		}
	}
}
