using Ardalis.ApiEndpoints;
using AutoMapper;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Identity.Api.AuthEndpoints
{
	public class Register : BaseEndpoint<RegisterUserRequest, RegisterUserResponse>
	{
		private IAuthService _authService;
		private IMapper _mapper;

		public Register(IAuthService authService, IMapper mapper) => 
			(_authService, _mapper) = (authService, mapper);

		[HttpPost("register")]
		[SwaggerOperation(
			Summary = "Register a user",
			Description = "Register a user",
			OperationId = "auth.register",
			Tags = new[] { "AuthEndpoints" })
		]
		public override ActionResult<RegisterUserResponse> Handle(RegisterUserRequest request)
		{
			if (request is null)
				return BadRequest(new RegisterUserResponse("Objeto não válido."));

			var result = _authService.Register(_mapper.Map<ApplicationUser>(request), request.Password, request.ConfirmPassword).Result;

			return Ok(new RegisterUserResponse(result.Errors));
		}
	}
}
