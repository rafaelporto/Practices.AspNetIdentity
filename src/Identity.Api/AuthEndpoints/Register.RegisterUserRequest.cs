using System.ComponentModel.DataAnnotations;

namespace Identity.Api.AuthEndpoints
{
    public class RegisterUserRequest
    {
		public string Name { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
