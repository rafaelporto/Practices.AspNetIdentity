using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infraestructure
{
	public class UserContext : IdentityDbContext
	{
		public UserContext(DbContextOptions<UserContext> options)
			: base(options)
		{
		}
	}
}
