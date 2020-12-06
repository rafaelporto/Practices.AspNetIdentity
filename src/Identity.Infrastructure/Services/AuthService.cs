using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infrastructure.Entities;
using Identity.Infrastructure.Jwt;
using Identity.Infrastructure.Jwt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure
{
	internal class AuthService : IAuthService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtBuilder _jwtBuilder;
		public AuthService(UserManager<ApplicationUser> userManager,
							SignInManager<ApplicationUser> signInManager,
							IOptions<AppJwtSettings> jwtSettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtBuilder = new JwtBuilder().WithUserManager(_userManager)
									.WithJwtSettings(jwtSettings.Value);
		}

		public async Task<Result<UserResponse, IEnumerable<string>>> Login(string email, string password)
		{
			if (email is null or "")
				return Result.Failure<UserResponse, IEnumerable<string>>(new string[] { "O nome do usuário é obrigatório"});
			
			if (password is null or "")
				return Result.Failure<UserResponse, IEnumerable<string>>(new string[] { "O password do usuário é obrigatório" });

			var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

			if (result.Succeeded)
			{
				var userResponse = _jwtBuilder.WithEmail(email).WithJwtClaims().WithUserClaims().BuildUserResponse();
				return Result.Success<UserResponse, IEnumerable<string>>(userResponse);
			}

			return Result.Failure<UserResponse, IEnumerable<string>>(new string[] { "Não foi possível fazer o login" });
		}
		//TODO: Implementar método de confirmação de conta
	}
}
