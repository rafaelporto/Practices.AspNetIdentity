using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infrastructure.Jwt.Model;

namespace Identity.Infrastructure
{
	public interface IAuthService
    {
        Task<Result<UserResponse, IEnumerable<string>>> Login(string email, string password);
    }
}
