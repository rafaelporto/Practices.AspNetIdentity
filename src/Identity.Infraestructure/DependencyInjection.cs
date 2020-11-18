using System;
using AutoMapper;
using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infraestructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection ConfigureServices(this IServiceCollection services)
		{
			services.AddDbContext<UserContext>()
					.AddEntityFrameworkInMemoryDatabase();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings.
				options.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				options.User.RequireUniqueEmail = false;
			});

			services.AddIdentity<User, IdentityRole>().AddEntityFrameworkStores<UserContext>();

			services.AddScoped<IAuthService, AuthService>();

			return services;
		}

		public static IApplicationBuilder UseIdentity(this IApplicationBuilder app)
		{
			return app;
		}
	}
}
