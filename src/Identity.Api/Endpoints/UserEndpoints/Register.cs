using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CSharpFunctionalExtensions;
using System.Collections.Generic;
using Identity.Api.Endpoints;
using System.Net;
using System.Threading.Tasks;

namespace Identity.Api.UserEndpoints
{
	[Produces("application/json")]
	public class Register : BaseAsyncEndpoint
	{
		[HttpPost("register")]
		[ProducesResponseType(typeof(RegisterUserOkResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(RegisterUserBadResponse), (int)HttpStatusCode.BadRequest)]
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "This endpoint is for a new user register yourself",
			OperationId = "auth.register",
			Tags = new[] { "UserEndpoints" })
		]
		public async Task<ActionResult<IResponse>> HandleAsync(RegisterUserRequest request,
			[FromServices] IUserService userService, [FromServices] IMapper mapper)
		{
			if (request is null)
				return BadRequest(new RegisterUserBadResponse("Bad parameters request."));

			return await userService.Register(mapper.Map<ApplicationUser>(request), request.Password, request.ConfirmPassword)
									 .Finally(MapResult);
		}

		private ActionResult<IResponse> MapResult(Result<ApplicationUser, IEnumerable<string>> result) =>
			 result.IsSuccess ?
				Ok(new RegisterUserOkResponse()) :
				BadRequest(new RegisterUserBadResponse(result.Error));
	}
}
