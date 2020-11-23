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
	public class AuthService : IAuthService
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

		//TODO: Implementar método de confirmação de conta
	}
}
