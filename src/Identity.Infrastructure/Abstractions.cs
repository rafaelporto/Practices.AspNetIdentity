using System;
using Identity.Infrastructure.Entities;
using Identity.Infrastructure.Jwt;
using Identity.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class Abstractions
	{
		public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			if (services is null)
				throw new ArgumentException($"{nameof(services)} is required for configure identity.");

			services.Configure<AppJwtSettings>(configuration.GetSection(AppJwtSettings.CONFIG_NAME));

			services.AddDbContext<UserContext>()
					.AddEntityFrameworkInMemoryDatabase();

			services.AddIdentity<ApplicationUser, ApplicationRole>(options => 
			{
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
				options.User.RequireUniqueEmail = false;

			}).AddEntityFrameworkStores<UserContext>();

			services.AddScoped<IAuthService, AuthService>();
			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IUserRepository, UserRepository>();

			return services;
		}

		public static IApplicationBuilder UseIdentityConfiguration(this IApplicationBuilder app)
		{
			if (app is null)
				throw new ArgumentException($"{nameof(app)} is required for configure identity middleware.");

			return app.UseAuthentication()
					  .UseAuthorization();
		}
	}
}
