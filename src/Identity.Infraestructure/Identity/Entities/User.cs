using Microsoft.AspNetCore.Identity;

namespace Identity.Infraestructure.Entities
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
		public string LastName { get; set; }
	}
}
