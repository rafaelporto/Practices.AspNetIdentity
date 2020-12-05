using System;
using System.Collections.Generic;
using System.Linq;
using Identity.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.UserEndpoints
{
	public struct CreateResponse : IResponse
	{
		public bool IsSuccess { get; }
		public IReadOnlyCollection<string> Notifications { get; }

		private CreateResponse(bool isSuccess, IEnumerable<string> errors = default) =>
			(IsSuccess, Notifications) = (isSuccess, errors?.ToArray());

		private CreateResponse(bool isSuccess, string error) =>
			(IsSuccess, Notifications) = (isSuccess, new string[] { error });

		public static IActionResult OkResponse() =>
			new OkObjectResult(new CreateResponse(true));

		public static IActionResult BadResponse(IEnumerable<string> errors) =>
			new BadRequestObjectResult(new CreateResponse(false, errors));

		public static IActionResult BadResponse(string error) =>
			new BadRequestObjectResult(new CreateResponse(false, error));
	}
}
