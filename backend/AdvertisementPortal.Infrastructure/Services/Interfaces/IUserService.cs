using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;
using Microsoft.AspNetCore.Http;

namespace AdvertisementPortal.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        bool TryAuthenticateUser(LoginModel user, out string token);
        UserModel? GetUserByUserName(string username);
        bool RegisterUser(RegisterModel userModel);
        RegisterModel? GetUserData(HttpRequest request);
    }
}
