using System;
using Identity.Data;
using Identity.Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.IoC
{
	public static class DependencyInjection
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddIdentityCore<User>(options =>
			{
				options.SignIn.RequireConfirmedAccount = true;
				options.SignIn.RequireConfirmedEmail = true;
				options.User.RequireUniqueEmail = true;
				options.Password.RequiredLength = 8;
			}).AddEntityFrameworkStores<UserContext>();

			services.AddDbContext<UserContext>()
					.AddEntityFrameworkInMemoryDatabase();
			
			return services;
		}

		public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
		{
			app.UseAuthentication();

			return app;
		}
	}
}
