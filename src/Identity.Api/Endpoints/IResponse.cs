using System.Collections.Generic;

namespace Identity.Api.Endpoints
{
	internal interface IResponse
	{
		public bool IsSuccess { get; }
		public IReadOnlyCollection<string> Notifications { get; }
	}
}
