using Identity.Infraestructure.Jwt.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Identity.Api.AuthEndpoints
{
	public readonly struct LoginResponse : ILoginResponse
	{
		public bool IsSuccess => true;
		public string AccessToken { get; }
		public double ExpiresIn { get; }
		public Guid Id { get; }
		public string Email { get; }
		public IReadOnlyCollection<UserClaim> Claims { get; }

		public LoginResponse(UserResponse userResponse)
        {
            AccessToken = userResponse.AccessToken;
			ExpiresIn = userResponse.ExpiresIn;
			Id = userResponse.UserToken.Id;
			Email = userResponse.UserToken.Email;
			Claims = userResponse.UserToken.Claims.ToArray();
        }
	}
}
