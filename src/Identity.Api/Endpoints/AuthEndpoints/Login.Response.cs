using Identity.Api.Endpoints;
using Identity.Infraestructure.Jwt.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.AuthEndpoints
{
	public readonly struct LoginResponse : IResponse
	{
		public bool IsSuccess { get; }
		public string AccessToken { get; }
		public double? ExpiresIn { get; }
		public Guid? Id { get; }
		public string Email { get; }
		public IReadOnlyCollection<UserClaim> Claims { get; }
		public IReadOnlyCollection<string> Notifications { get; }


		private LoginResponse(bool isSuccess, UserResponse userResponse, IEnumerable<string> notifications)
        {
			IsSuccess = isSuccess;
            AccessToken = userResponse?.AccessToken;
			ExpiresIn = userResponse?.ExpiresIn;
			Id = userResponse?.UserToken?.Id;
			Email = userResponse?.UserToken?.Email;
			Claims = userResponse?.UserToken?.Claims?.ToArray();
			Notifications = notifications?.ToArray();
        }

		public static IActionResult OkResponse(UserResponse userResponse,
						IEnumerable<string> notifications = default) =>
			new OkObjectResult(new LoginResponse(true, userResponse, notifications));

		public static IActionResult BadResponse(IEnumerable<string> notifications = default) =>
			new BadRequestObjectResult(new LoginResponse(false, default, notifications));
	}
}
