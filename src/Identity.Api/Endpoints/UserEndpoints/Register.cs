using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using CSharpFunctionalExtensions;
using System.Net;
using System.Threading.Tasks;

namespace Identity.Api.UserEndpoints
{
	[Produces("application/json")]
	public class Register : BaseAsyncEndpoint
	{
		[HttpPost("user")]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.BadRequest)]
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "This endpoint is for a new user register yourself",
			OperationId = "user.register",
			Tags = new[] { "UserEndpoints" })
		]
		public async Task<IActionResult> HandleAsync(RegisterUserRequest request,
			[FromServices] IUserService userService, [FromServices] IMapper mapper)
		{
			if (request is null)
				return RegisterUserResponse.BadResponse("Bad parameters request.");

			return await userService.Register(mapper.Map<ApplicationUser>(request), 
												request.Password,
												request.ConfirmPassword)
									 .Finally(result => result.IsSuccess ?
												RegisterUserResponse.OkResponse() :
												RegisterUserResponse.BadResponse(result.Error));
		}
	}
}
