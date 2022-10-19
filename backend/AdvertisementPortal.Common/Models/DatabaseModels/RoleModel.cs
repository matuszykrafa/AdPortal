using AdvertisementPortal.Common.Auth;

namespace AdvertisementPortal.Common.Models.DatabaseModels
{
    public class RoleModel
    {
        public int Id { get; set; }
        public UserType RoleType { get; set; }
        public string RoleName { get; set; }
    }
}
