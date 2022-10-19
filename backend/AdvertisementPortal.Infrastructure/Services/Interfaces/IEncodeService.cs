using AdvertisementPortal.Common.Models.DatabaseModels;

namespace AdvertisementPortal.Infrastructure.Services.Interfaces
{
    public interface IEncodeService
    {
        void EncodeUser(UserModel userModel, string password);
        byte[] GetHash(byte[] salt, string password);
    }
}
