using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infraestructure.Entities;

namespace Identity.Infraestructure
{
	public interface IUserRepository
	{
		Task<Maybe<IEnumerable<ApplicationUser>>> GetUsers(CancellationToken cancellationToken = default);
	}
}