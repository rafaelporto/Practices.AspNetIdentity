using System.Threading.Tasks;
using Identity.Infraestructure.Jwt.Model;

namespace Identity.Infraestructure.Services
{
	public interface IAuthService
    {
        Task<Result<UserResponse>> Login(string email, string password);
    }
}
