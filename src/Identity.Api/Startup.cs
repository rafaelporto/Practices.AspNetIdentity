using AutoMapper;
using FluentValidation;
using Identity.Api.Configurations;
using Identity.Infrastructure;
using Identity.Infrastructure.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace Identity.Api
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			ValidatorOptions.Global.LanguageManager.Enabled = false;

			services.AddIdentityConfiguration(Configuration)
					.AddJwtConfiguration(Configuration)
					.AddAutoMapper(typeof(Startup))
					.AddControllers()
					.AddJsonOptions(options =>
					{
						options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
					});

			services.ConfigureSwagger();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwaggerConfiguration();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseIdentityConfiguration();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
