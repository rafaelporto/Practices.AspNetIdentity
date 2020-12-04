using System.Collections.Generic;
using System.Linq;
using Identity.Api.Endpoints;

namespace Identity.Api.AuthEndpoints
{
	public readonly struct LoginBadResponse : IResponse
	{
		public bool IsSuccess => false;
		public IReadOnlyCollection<string> Errors { get; }

		public LoginBadResponse(IEnumerable<string> errors) =>
			Errors = errors.ToArray();
		
		public LoginBadResponse(string error) =>
			Errors = new string[] { error };

	}
}
