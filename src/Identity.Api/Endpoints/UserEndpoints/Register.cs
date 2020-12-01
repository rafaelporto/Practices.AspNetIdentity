using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.UserEndpoints
{
	public class Register : BaseEndpoint<RegisterUserRequest, RegisterUserResponse>
	{
		private IUserService _userService;
		private IMapper _mapper;

		public Register(IUserService userService, IMapper mapper) => 
			(_userService, _mapper) = (userService, mapper);

		[HttpPost("register")]
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "This endpoint is for a new user register yourself",
			OperationId = "auth.register",
			Tags = new[] { "UserEndpoints" })
		]
		public override ActionResult<RegisterUserResponse> Handle(RegisterUserRequest request)
		{
			if (request is null)
				return BadRequest(new RegisterUserResponse("Objeto não válido."));

			var result = _userService.Register(_mapper.Map<ApplicationUser>(request), request.Password, request.ConfirmPassword).Result;

			return Ok(new RegisterUserResponse(result.Errors));
		}
	}
}
