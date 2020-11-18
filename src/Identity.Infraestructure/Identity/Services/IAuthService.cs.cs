using System.Threading.Tasks;
using Identity.Infraestructure.Entities;

namespace Identity.Infraestructure.Services
{
    public interface IAuthService
    {
        Task<Result<bool>> Login(string email, string password);
        Task<Result<bool>> Register(User newUser, string password);
    }
}
