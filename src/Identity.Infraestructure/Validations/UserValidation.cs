using FluentValidation;
using Identity.Infraestructure.Entities;

namespace Identity.Infraestructure.Validations
{
	public class UserValidation : AbstractValidator<ApplicationUser>
	{
		public UserValidation()
		{
			RuleFor(p => p.Email).EmailAddress();
			RuleFor(p => p.Name)
				.Cascade(CascadeMode.Stop)
				.NotNull().NotEmpty();
		}
	}
}
