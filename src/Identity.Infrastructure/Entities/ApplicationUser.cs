using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Identity.Infrastructure.Validations;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
	{
		public static ApplicationUser NewUser(string firstName, string lastName, string email, string phoneNumber) =>
			new ApplicationUser
			{
				FirstName = firstName,
				LastName = lastName,
				Email = email,
				UserName = email,
				PhoneNumber = phoneNumber
			};

		public string FirstName { get; init; }
		public string LastName { get; init; }
		public IReadOnlyList<string> Notifications => Validations?.Errors?.Select(p => p.ErrorMessage).ToList();
		private ValidationResult Validations => new UserValidation().Validate(this);
		public bool IsValid => Validations.IsValid;
		public bool IsInvalid => !IsValid;
	}
}
