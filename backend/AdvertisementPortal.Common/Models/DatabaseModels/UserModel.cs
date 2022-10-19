using AdvertisementPortal.Common.Auth;
using System.Collections.Generic;

namespace AdvertisementPortal.Common.Models.DatabaseModels
{
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public virtual IEnumerable<OfferModel> OfferModels { get; set; }
    }
}
