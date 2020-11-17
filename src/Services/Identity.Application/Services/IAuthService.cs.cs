using Identity.Core;
using System.Threading.Tasks;

namespace Identity.Application.Services
{
    public interface IAuthService
    {
        Task<Result<bool>> Login(string email, string password);
        Task<Result<bool>> Register(string email, string password);
    }
}
