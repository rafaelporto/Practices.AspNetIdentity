using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infrastructure.Entities;

namespace Identity.Infrastructure
{
	public interface IUserRepository
	{
		Task<Maybe<IEnumerable<ApplicationUser>>> GetUsers(CancellationToken cancellationToken = default);
	}
}