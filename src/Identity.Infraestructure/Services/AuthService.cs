using System;
using System.Linq;
using System.Threading.Tasks;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Jwt;
using Identity.Infraestructure.Jwt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Identity.Infraestructure.Services
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

		public async Task<Result<UserResponse>> Login(string email, string password)
		{
			if (email is null or "")
				return Result.Failure<UserResponse>("O nome do usuário é obrigatório");
			
			if (password is null or "")
				return Result.Failure<UserResponse>("O password do usuário é obrigatório");

			var result = await _signInManager.PasswordSignInAsync(email, password, false, true);

			if (result.Succeeded)
			{
				var userResponse = _jwtBuilder.WithEmail(email).WithJwtClaims().WithUserClaims().BuildUserResponse();
				return Result.Success(userResponse);
			}

			return Result.Failure<UserResponse>("Não foi possível fazer o login");
		}
		//TODO: Implementar método de confirmação de conta
	}
}
