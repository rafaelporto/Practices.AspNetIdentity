using System.Collections.Generic;
using System.Linq;
using Identity.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.UserEndpoints
{
	public readonly struct RegisterUserResponse : IResponse
	{
		public bool IsSuccess { get; }
		public IReadOnlyCollection<string> Notifications { get; }

		private RegisterUserResponse(bool isSuccess, IEnumerable<string> errors = default) =>
			(IsSuccess, Notifications) = (isSuccess, errors?.ToArray());

		private RegisterUserResponse(bool isSuccess, string error) =>
			(IsSuccess, Notifications) = (isSuccess, new string[] { error });

		public static IActionResult OkResponse() =>
			new OkObjectResult(new RegisterUserResponse(true));

		public static IActionResult BadResponse(IEnumerable<string> errors) =>
			new BadRequestObjectResult(new RegisterUserResponse(false, errors));

		public static IActionResult BadResponse(string error) =>
			new BadRequestObjectResult(new RegisterUserResponse(false, error));
	}
}
