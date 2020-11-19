using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.AuthEndpoints
{
	public record RegisterUserResponse
	{
		public bool IsSuccess => Errors?.Any() == false;
		public IReadOnlyCollection<string> Errors { get; set; }

		public RegisterUserResponse(IEnumerable<string> errors) =>
			Errors = errors.ToArray();

		public RegisterUserResponse(string error) =>
			Errors = new string[] { error };
	}
}
