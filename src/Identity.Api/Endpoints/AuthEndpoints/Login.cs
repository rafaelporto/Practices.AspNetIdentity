using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using CSharpFunctionalExtensions;
using Identity.Api.Endpoints;
using Identity.Infraestructure.Jwt.Model;
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
		[ProducesResponseType(typeof(LoginOkResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(LoginBadResponse), (int)HttpStatusCode.BadRequest)]
		public async Task<ActionResult<IResponse>> HandleAsync(LoginRequest request,
			[FromServices] IAuthService authService)
		{
			return await authService.Login(request.Email, request.Password)
									.Finally(MapResult);
		}

		private ActionResult<IResponse> MapResult(Result<UserResponse, IEnumerable<string>> result) =>
			result.IsSuccess ?
				Ok(new LoginOkResponse(result.Value)) :
				BadRequest(new LoginBadResponse(result.Error));
	}
}
