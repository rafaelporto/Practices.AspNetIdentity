using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.UserEndpoints
{
	public class Create : BaseEndpoint<CreateRequest, ICreateResponse>
	{
		private IUserService _userService;
		private IMapper _mapper;

		public Create(IUserService userService, IMapper mapper) =>
			(_userService, _mapper) = (userService, mapper);

		[HttpPost("users")]
		[SwaggerOperation(
			Summary = "Create a user",
			Description = "Create a new user",
			OperationId = "users.create",
			Tags = new[] { "UserEndpoints" })
		]
		public override ActionResult<ICreateResponse> Handle(CreateRequest request)
		{
			var result = _userService.CreateUser();

			return Ok();
		}
	}
}
