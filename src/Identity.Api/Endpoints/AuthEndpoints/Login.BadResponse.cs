using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.AuthEndpoints
{
	public readonly struct LoginBadResponse : ILoginResponse
	{
		public bool IsSuccess => false;
		public IReadOnlyCollection<string> Errors { get; }

		public LoginBadResponse(IEnumerable<string> errors) =>
			Errors = errors.ToArray();

	}
}
