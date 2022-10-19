using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using System.Security.Cryptography;

namespace AdvertisementPortal.Infrastructure.Services.Implementations
{
    public class EncodeService : IEncodeService
    {
        public void EncodeUser(UserModel userModel, string password)
        {
            var salt = CreateSalt();
            var hash = GetHash(salt, password);
            userModel.Salt = salt;
            userModel.Hash = hash;
        }
        private byte[] CreateSalt()
        {
            var salt = new byte[32];
            var rngCrypto = RandomNumberGenerator.Create();
            rngCrypto.GetBytes(salt);
            return salt;
        }

        public byte[] GetHash(byte[] salt, string password)
        {
            var hash = new Rfc2898DeriveBytes(password, salt, 1000).GetBytes(32);
            return hash;
        }
    }
}
