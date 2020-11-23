using Ardalis.ApiEndpoints;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.AuthEndpoints
{
	public class Login : BaseEndpoint<LoginRequest, ILoginResponse>
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
		[ProducesResponseType(typeof(LoginResponse), 200)]
		[ProducesResponseType(typeof(LoginBadResponse), 404)]
		public override ActionResult<ILoginResponse> Handle(LoginRequest request)
		{
			var result = _authService.Login(request.Email, request.Password).Result;

            if (result.IsSuccess)
				return Ok(new LoginResponse(result.Value));

			return BadRequest(new LoginBadResponse(result.Errors));
		}
	}
}
