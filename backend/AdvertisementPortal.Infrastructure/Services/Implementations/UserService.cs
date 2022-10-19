using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementPortal.Infrastructure.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ITokenService _tokenService;
        private readonly IEncodeService _encodeService;
        public UserService(ApplicationDbContext applicationDbContext, ITokenService tokenService, IEncodeService encodeService)
        {
            _applicationDbContext = applicationDbContext;
            _tokenService = tokenService;
            _encodeService = encodeService;
        }

        public bool TryAuthenticateUser(LoginModel user, out string token)
        {
            token = String.Empty;
            var userModel = GetUserByUserName(user.UserName);
            if (userModel is null) return false;
            var providedPassword = _encodeService.GetHash(userModel.Salt, user.Password);

            if (!userModel.Hash.SequenceEqual(providedPassword))
                return false;

            token = _tokenService.CreateToken(userModel);
            return true;
        }

        public UserModel? GetUserByUserName(string username)
        {
            return _applicationDbContext.Users.FirstOrDefault(x => x.UserName == username);
        }

        public bool RegisterUser(RegisterModel registerModel)
        {
            var newUser = new UserModel()
            {
                UserName = registerModel.UserName,
                UserType = Common.Auth.UserType.USER,
                Email = registerModel.Email,
                PhoneNumber = registerModel.PhoneNumber,
                IsActive = true
            };
            _encodeService.EncodeUser(newUser, registerModel.Password);
            _applicationDbContext.Add(newUser);
            _applicationDbContext.SaveChanges();
            return true;
        }

        public RegisterModel? GetUserData(HttpRequest request)
        {
            var userId = _tokenService.GetAuthorId(request);
            if (!userId.HasValue) return null;
            var user = _applicationDbContext.Users.Where(x => x.Id == userId.Value).FirstOrDefault();
            if (user is null) return null;
            var registerModel = new RegisterModel()
            {
                UserName = user.UserName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            return registerModel;
        }
    }
}
