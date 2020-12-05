using System.Net;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using CSharpFunctionalExtensions;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.AuthEndpoints
{
	[Produces("application/json")]
	public class Login : BaseAsyncEndpoint
	{
		[HttpPost("login")]
		[SwaggerOperation(
			Summary = "Login",
			Description = "Login a user",
			OperationId = "auth.login",
			Tags = new[] { "AuthEndpoints" })
		]
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> HandleAsync(LoginRequest request,
			[FromServices] IAuthService authService)
		{
			return await authService.Login(request.Email, request.Password)
									.Finally(result => result.IsSuccess ?
												LoginResponse.OkResponse(result.Value) :
												LoginResponse.BadResponse(result.Error));
		}
	}
}
