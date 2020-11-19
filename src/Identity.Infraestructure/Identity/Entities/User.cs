using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Identity.Infraestructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure.Entities
{
	public class User : IdentityUser
	{
		protected User() { }

		public static User NewUser(string name, string lastName, string email) =>
			new User
			{
				Name = name,
				LastName = lastName,
				Email = email
			};

		public string Name { get; init; }
		public string LastName { get; init; }
		public IReadOnlyList<string> Notifications => Validations?.Errors?.Select(p => p.ErrorMessage).ToList();
		
		private ValidationResult _validations;
		private ValidationResult Validations 
		{
			get
			{
				if (_validations is null)
					_validations = new UserValidation().Validate(this);

				return _validations;
			}
		}

		public bool IsValid => Validations.IsValid;
		public bool IsInvalid => !IsValid;
	}
}
