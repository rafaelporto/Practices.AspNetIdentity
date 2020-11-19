using FluentValidation;
using Identity.Infraestructure.Entities;

namespace Identity.Infraestructure.Identity
{
	public class UserValidation : AbstractValidator<User>
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
