namespace Identity.Api.UserEndpoints
{
	public record UserDto
	{
		public string FirstName { get; init; }
		public string LastName { get; init; }
		public string Email { get; init; }
		public string PhoneNumber { get; init; }
		public string UserName { get; init; }

		//public UserDto(string firstName, string lastName, string email, string phoneNumber, string userName)
		//{
		//	FirstName = firstName;
		//	LastName = lastName;
		//	Email = email;
		//	PhoneNumber = phoneNumber;
		//	UserName = userName;
		//}
	}
}
