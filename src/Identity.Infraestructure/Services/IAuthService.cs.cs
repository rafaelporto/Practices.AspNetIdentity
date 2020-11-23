using System.Threading.Tasks;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Jwt.Model;

namespace Identity.Infraestructure.Services
{
    public interface IAuthService
    {
        Task<Result<UserResponse>> Login(string email, string password);
        Task<Result> Register(ApplicationUser newUser, string password, string confirmedPassword);
    }
}
