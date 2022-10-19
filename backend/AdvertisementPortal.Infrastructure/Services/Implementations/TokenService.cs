using AdvertisementPortal.Common.Auth;
using AdvertisementPortal.Common.Enums;
using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AdvertisementPortal.Infrastructure.Services.Implementations
{
    public class TokenService : ITokenService
    {
        public static readonly SymmetricSecurityKey SecretKey = new(Encoding.UTF8.GetBytes("wqWLEYfsB7DsvmPs5Gxhk8mdCUNLNQ6yQYD@345"));
        private readonly ApplicationDbContext _applicationDbContext;

        public TokenService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public string CreateToken(UserModel user)
        {

            var signinCredentials = new SigningCredentials(SecretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
                issuer: "https://localhost:7007",
                audience: "https://localhost:7007",
                claims: GetUserClaims(user),
                expires: DateTime.Now.AddHours(1),
                signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }

        private static List<Claim> GetUserClaims(UserModel user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName ?? "emptyLogin"),
                new Claim(ClaimTypes.Role, user.UserType.ToString()),
            };
        }

        public bool ValidateClaims(HttpRequest request, ClaimType claimType, int objectId)
        {
            var isToken = TryGetToken(request, out var token);
            if (!isToken) return false;
            var claims = GetClaims(token);

            var userNameClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            var roleClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (userNameClaim is null || roleClaim is null) return false;

            var author = _applicationDbContext.Users.Include(x => x.OfferModels).FirstOrDefault(x => x.UserName == userNameClaim.Value);
            if (author is null) return false;

            if (author.UserType == Common.Auth.UserType.ADMIN) 
                return true;

            if (claimType == ClaimType.OFFER)
            {
                return ValidateOffer(objectId, author);
            }
            return false;
        }

        private bool ValidateOffer(int objectId, UserModel author)
        {
            if (objectId == 0)
                return true;
            if (!author.OfferModels.Select(x => x.Id).Contains(objectId))
                return false;
            return true;
        }

        private bool TryGetToken(HttpRequest request, out string token)
        {
            token = String.Empty;
            var accessToken = request.Headers[HeaderNames.Authorization].Where(x => x.StartsWith("Bearer ")).FirstOrDefault();
            if (string.IsNullOrEmpty(accessToken)) return false;

            token = accessToken[7..];
            return true;
        }

        private IEnumerable<Claim> GetClaims(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            return handler.ReadJwtToken(token).Claims;
        }

        public int? GetAuthorId(HttpRequest request)
        {
            var isToken = TryGetToken(request, out var token);
            if (!isToken) return null;
            var claims = GetClaims(token);
            var authorName = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
            if (authorName == null) return null;
            var author = _applicationDbContext.Users.Where(x => x.UserName == authorName.Value).FirstOrDefault();
            if (author is null) return null;
            return author.Id;
        }

        public bool IsAdmin(HttpRequest request)
        {
            var isToken = TryGetToken(request, out var token);
            if (!isToken) return false;
            var claims = GetClaims(token);
            var authorRole = claims.FirstOrDefault(x => x.Type == ClaimTypes.Role);
            if (authorRole == null) return false;
            else if (authorRole.Value == UserType.ADMIN.ToString()) return true;
            return false;
        }
    }
}
