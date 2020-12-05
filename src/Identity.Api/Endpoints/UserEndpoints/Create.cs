using System.Collections.Generic;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using CSharpFunctionalExtensions;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.UserEndpoints
{
	[Produces("application/json")]
	public class Create : BaseAsyncEndpoint
	{
		[HttpPost("users")]
		[SwaggerOperation(
			Summary = "Create a user",
			Description = "Create a new user",
			OperationId = "users.create",
			Tags = new[] { "UserEndpoints" })
		]
		public async Task<IActionResult> HandleAsync([FromBody] CreateRequest request,
			[FromServices] IUserService userService, [FromServices] IMapper mapper)
		{
			return await userService.CreateUser(mapper.Map<ApplicationUser>(request),
												request.Password,
												request.ConfirmPassword)
									.Finally(result => result.IsSuccess ?
												CreateResponse.OkResponse() :
												CreateResponse.BadResponse(result.Error));
		}
	}
}
