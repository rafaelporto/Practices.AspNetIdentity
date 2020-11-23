using AutoMapper;
using FluentValidation;
using Identity.Infraestructure;
using Identity.Infraestructure.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;

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
					.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.Api", Version = "v1" });
				c.AddSecurityDefinition("Bearer",
		new OpenApiSecurityScheme
						{
							In = ParameterLocation.Header,
							Description = "Please enter into field the word 'Bearer' following by space and JWT",
							Name = "Authorization",
							Type = SecuritySchemeType.ApiKey
						});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement 
				{
				   {
					 new OpenApiSecurityScheme
					 {
					   Reference = new OpenApiReference
					   {
						 Type = ReferenceType.SecurityScheme,
						 Id = "Bearer"
					   }
					  },
					  Array.Empty<string>()
				   }
				});
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.Web v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseIdentity();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
