using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infraestructure.Jwt.Model;

namespace Identity.Infraestructure.Services
{
	public interface IAuthService
    {
        Task<Result<UserResponse, IEnumerable<string>>> Login(string email, string password);
    }
}
