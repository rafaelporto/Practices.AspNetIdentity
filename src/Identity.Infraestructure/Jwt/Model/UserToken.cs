using System;
using System.Collections.Generic;

namespace Identity.Infraestructure.Jwt.Model
{
    public class UserToken
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public IEnumerable<UserClaim> Claims { get; set; }
    }
}
