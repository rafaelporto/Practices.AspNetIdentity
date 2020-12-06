using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.UserEndpoints
{
	[Authorize]
	public class List : BaseAsyncEndpoint
	{
		[HttpGet("users")]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(RegisterUserResponse), (int)HttpStatusCode.BadRequest)]
		[SwaggerOperation(
			Summary = "Get all users",
			Description = "This endpoint is for get all users",
			OperationId = "user.list",
			Tags = new[] { "UserEndpoints" })
		]
		public async Task<IActionResult> HandleAsync([FromServices] IUserRepository userRepository,
														[FromServices] IMapper mapper,
														CancellationToken cancellationToken = default)
		{
			var maybeUsers = await userRepository.GetUsers(cancellationToken);

			if (maybeUsers.HasValue)
				return ListResponse.OkResponse(mapper.Map<IEnumerable<UserDto>>(maybeUsers.Value));

			return ListResponse.OkResponse(default);
		}
	}
}
