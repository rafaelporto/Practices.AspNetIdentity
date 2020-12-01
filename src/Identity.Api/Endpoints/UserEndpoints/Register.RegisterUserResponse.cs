using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.UserEndpoints
{
	public readonly struct RegisterUserResponse
	{
		public bool IsSuccess => Errors is null || !Errors.Any();
		public IReadOnlyCollection<string> Errors { get; }

		public RegisterUserResponse(IEnumerable<string> errors) =>
			Errors = errors?.ToArray();

		public RegisterUserResponse(string error) =>
			Errors = new string[] { error };
	}
}
