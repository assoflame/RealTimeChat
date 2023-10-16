using DataAccess.Interfaces;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Interfaces;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IRepoManager _repoManager;
        private readonly IConfiguration _configuration;

        private User? _user;

        public AuthService(IRepoManager repoManager, IConfiguration configuration)
        {
            _repoManager = repoManager;
            _configuration = configuration;
        }

        public async Task SignUpAsync(SignUpDto signUpDto)
        {
            var userWithSameNickname = (await _repoManager.Users
                .FindByConditionAsync(user => user.Nickname.Equals(signUpDto.Nickname)))
                .FirstOrDefault();

            if (userWithSameNickname is not null)
                throw new ArgumentException(); // user with this nickname already exists. throw custom exception

            var passwordSalt = signUpDto.Nickname;
            var passwordHash = ComputeMD5HashString(signUpDto.Password + passwordSalt);

            var user = new User()
            {
                Nickname = signUpDto.Nickname,
                PasswordHash = passwordHash
            };

            await _repoManager.Users.CreateAsync(user);
        }

        public async Task<bool> ValidateUserAsync(SignInDto signInDto)
        {
            _user = (await _repoManager.Users
                .FindByConditionAsync(user => user.Nickname.Equals(signInDto.Nickname)))
                .FirstOrDefault();

            if (_user is null)
                return false;

            var passwordSalt = _user.Nickname;
            var passwordHash = ComputeMD5HashString(signInDto.Password + passwordSalt);

            return _user.PasswordHash.Equals(passwordHash);
        }

        public string CreateToken()
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            var claims = GetClaims();
            var token = new JwtSecurityToken(
                    issuer: jwtSettings["validIssuer"],
                    audience: jwtSettings["validAudience"],
                    claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(int.Parse(jwtSettings["expires"]))),
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["secretKey"])),
                        SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private List<Claim> GetClaims()
        {
            var claims = new List<Claim>();

            if (_user is not null)
            {
                claims.Add(new Claim("Nickname", _user.Nickname));
                claims.Add(new Claim("Id", _user.Id));
            }

            return claims;
        }

        private string ComputeMD5HashString(string str)
        {
            var md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));

            return Convert.ToHexString(hash);
        }
    }
}
