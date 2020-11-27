﻿using System;

namespace Identity.Infraestructure.Jwt.Model
{
    public class UserResponse
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
