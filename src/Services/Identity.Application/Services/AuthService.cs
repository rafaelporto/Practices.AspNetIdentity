using System.Threading.Tasks;
using Identity.Core;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Application.Services
{
	public class AuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		public AuthService(UserManager<User> userManager, SignInManager<User> signInManager) =>
			(_userManager, _signInManager) = (userManager, signInManager);

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
	}
}
