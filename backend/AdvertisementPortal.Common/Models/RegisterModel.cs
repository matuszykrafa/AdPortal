using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementPortal.Common.Models
{
    public class RegisterModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
