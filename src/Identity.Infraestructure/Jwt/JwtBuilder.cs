using Identity.Infraestructure.Entities;
using Identity.Infraestructure.Jwt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Identity.Infraestructure.Jwt
{
    public class JwtBuilder
    {
        private UserManager<ApplicationUser> _userManager;
        private AppJwtSettings _appJwtSettings;
        private ApplicationUser _user;
        private ICollection<Claim> _userClaims;
        private ICollection<Claim> _jwtClaims;
        private ClaimsIdentity _identityClaims;

        public JwtBuilder WithUserManager(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager ?? throw new ArgumentException(nameof(userManager));
            return this;
        }

        public JwtBuilder WithJwtSettings(AppJwtSettings appJwtSettings)
        {
            _appJwtSettings = appJwtSettings ?? throw new ArgumentException(nameof(appJwtSettings));
            return this;
        }

        public JwtBuilder WithEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentException(nameof(email));

            if (_userManager is null)
                throw new InvalidOperationException("UserManager should not be null.");

            _user = _userManager.FindByEmailAsync(email).Result;
            _userClaims = new List<Claim>();
            _jwtClaims = new List<Claim>();
            _identityClaims = new ClaimsIdentity();

            return this;
        }

        public JwtBuilder WithJwtClaims()
        {
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, _user.Id.ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Email, _user.Email));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpochDate(DateTime.UtcNow).ToString()));
            _jwtClaims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(DateTime.UtcNow).ToString(), ClaimValueTypes.Integer64));

            _identityClaims.AddClaims(_jwtClaims);

            return this;
        }

        public JwtBuilder WithUserClaims()
        {
            _userClaims = _userManager.GetClaimsAsync(_user).Result;
            _identityClaims.AddClaims(_userClaims);

            return this;
        }

        public JwtBuilder WithUserRoles()
        {
            var userRoles = _userManager.GetRolesAsync(_user).Result;
            userRoles.ToList().ForEach(r => _identityClaims.AddClaim(new Claim("role", r)));

            return this;
        }

        public string BuildToken()
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appJwtSettings.SecretKey);
            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _appJwtSettings.Issuer,
                Audience = _appJwtSettings.Audience,
                Subject = _identityClaims,
                Expires = DateTime.UtcNow.AddHours(_appJwtSettings.Expiration),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            });

            return tokenHandler.WriteToken(token);
        }

        public UserResponse BuildUserResponse()
        {
            var user = new UserResponse
            {
                AccessToken = BuildToken(),
                ExpiresIn = TimeSpan.FromHours(_appJwtSettings.Expiration).TotalSeconds,
                UserToken = new UserToken
                {
                    Id = _user.Id,
                    Email = _user.Email,
                    Claims = _userClaims.Select(c => new UserClaim { Type = c.Type, Value = c.Value })
                }
            };

            return user;
        }

        private static long ToUnixEpochDate(DateTime date)
            => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
    }
}
