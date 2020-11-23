using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Identity.Infraestructure.Validations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
	{
		protected ApplicationUser() { }

		public static ApplicationUser NewUser(string name, string lastName, string email) =>
			new ApplicationUser
			{
				Name = name,
				LastName = lastName,
				Email = email,
				UserName = email
			};

		public string Name { get; init; }
		public string LastName { get; init; }
		public IReadOnlyList<string> Notifications => Validations?.Errors?.Select(p => p.ErrorMessage).ToList();
		private ValidationResult Validations => new UserValidation().Validate(this);
		public bool IsValid => Validations.IsValid;
		public bool IsInvalid => !IsValid;
	}
}
