using AdvertisementPortal.Common.Enums;
using AdvertisementPortal.Common.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;

namespace AdvertisementPortal.Infrastructure.Services.Interfaces
{
    public interface ITokenService
    {
        public string CreateToken(UserModel user);
        bool ValidateClaims(HttpRequest request, ClaimType claimType, int offerId);
        int? GetAuthorId(HttpRequest request);
        bool IsAdmin(HttpRequest request);
    }
}
