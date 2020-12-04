using System.Collections.Generic;
using System.Linq;
using Identity.Api.Endpoints;

namespace Identity.Api.UserEndpoints
{
	public readonly struct RegisterUserBadResponse : IResponse
	{
		public bool IsSuccess => Errors is null || !Errors.Any();
		public IReadOnlyCollection<string> Errors { get; }

		public RegisterUserBadResponse(IEnumerable<string> errors = default) =>
			Errors = errors?.ToArray();

		public RegisterUserBadResponse(string error = default) =>
			Errors = new string[] { error };
	}
}
