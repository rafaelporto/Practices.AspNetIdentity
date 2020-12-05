using System.Collections.Generic;
using System.Linq;
using Identity.Api.Endpoints;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.UserEndpoints
{
	public struct ListResponse : IResponse
	{
		public bool IsSuccess { get; }

		public IReadOnlyCollection<UserDto> Users { get; }

		public IReadOnlyCollection<string> Notifications { get; }

		private ListResponse(bool isSuccess, IEnumerable<UserDto> users,
				IEnumerable<string> notifications = default) =>
			(IsSuccess, Users, Notifications) = (isSuccess, users.ToArray(), notifications?.ToArray());

		public static IActionResult OkResponse(IEnumerable<UserDto> users,
						IEnumerable<string> notifications = default) =>
			new OkObjectResult(new ListResponse(true, users, notifications));
	}
}
