using Ardalis.ApiEndpoints;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.AuthEndpoints
{
	public class Login : BaseEndpoint<LoginRequest, LoginResponse>
	{
		public readonly IAuthService _authService;

		public Login(IAuthService authService) =>
			_authService = authService;


		[HttpPost("login")]
		[SwaggerOperation(
			Summary = "Login",
			Description = "Login a user",
			OperationId = "auth.login",
			Tags = new[] { "AuthEndpoints" })
		]
		public override ActionResult<LoginResponse> Handle(LoginRequest request)
		{
			var result = _authService.Login(request.Email, request.Password).Result;

			return Ok(new LoginResponse { IsSuccess = result.IsSuccess });
		}
	}
}
