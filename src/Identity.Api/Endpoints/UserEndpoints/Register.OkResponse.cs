using Identity.Api.Endpoints;

namespace Identity.Api.UserEndpoints
{
	public readonly struct RegisterUserOkResponse : IResponse
	{
		public bool IsSuccess => true;
	}
}
