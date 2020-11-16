using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
	public class User : IdentityUser
	{
		public string Name { get; set; }
		public string LastName { get; set; }
	}
}
