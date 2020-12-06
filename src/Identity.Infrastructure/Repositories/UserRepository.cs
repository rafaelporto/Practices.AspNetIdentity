using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Identity.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
	public class UserRepository : IUserRepository
	{
		private readonly UserContext _dbContext;

		public UserRepository(UserContext dbContext) =>
			_dbContext = dbContext;

		public async Task<Maybe<IEnumerable<ApplicationUser>>> GetUsers(CancellationToken cancellationToken = default) =>
			 await _dbContext.Users.AsNoTracking()
									.Select(s => new ApplicationUser()
									{ 
										FirstName = s.FirstName,
										LastName = s.LastName,
										Email = s.Email,
										PhoneNumber = s.PhoneNumber,
										UserName = s.UserName
									}).ToListAsync(cancellationToken);

		
	}
}
