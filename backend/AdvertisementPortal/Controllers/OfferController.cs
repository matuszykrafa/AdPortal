using AdvertisementPortal.Common.Auth;
using AdvertisementPortal.Common.Enums;
using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.IdentityModel.Tokens.Jwt;

namespace AdvertisementPortal.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController, Authorize(Roles = $"USER,ADMIN")]
    public class OfferController : ControllerBase
    {
        private readonly IOfferService _offerService;
        private readonly ITokenService _tokenService;

        public OfferController(IOfferService offerService, ITokenService tokenService)
        {
            _offerService = offerService;
            _tokenService = tokenService;
        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetOfferById(int offerId)
        {
            bool isAdmin = _tokenService.IsAdmin(Request);
            var res = _offerService.GetOfferById(offerId, isAdmin);
            return Ok(res);

        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetOffers()
        {
            var res = _offerService.GetOffers();
            return Ok(res);

        }

        [HttpGet, AllowAnonymous]
        public ActionResult GetOffersByCategory(int categoryId)
        {
            var res = _offerService.GetOffersByCategory(categoryId);
            return Ok(res);

        }

        [HttpGet]
        public ActionResult GetOfferByIdManagement(int offerId)
        {
            if(!_tokenService.ValidateClaims(Request, ClaimType.OFFER, offerId))
                return Unauthorized();
            
            var res = _offerService.GetOfferManagementById(offerId);
            return Ok(res);

        }
        [HttpGet, Authorize]
        public ActionResult GetOffersForUser()
        {
            var authorId = _tokenService.GetAuthorId(Request);
            if (!authorId.HasValue) return BadRequest("User does not exist");
            List<OfferModel> res = _offerService.GetOffersForUser((int)authorId);
            return Ok(res);

        }
        [HttpPost]
        public ActionResult AddOrUpdateOffer([FromBody] OfferModel offerModel)
        {
            var validate = _tokenService.ValidateClaims(Request, ClaimType.OFFER, offerModel.Id);
            if (!validate) return Unauthorized();

            var authorId = _tokenService.GetAuthorId(Request);
            if (!authorId.HasValue) return BadRequest("User does not exist");

            OfferModel res = _offerService.AddOrUpdateOffer((int)authorId, offerModel);
            return Ok(res);

        }

        [HttpPost]
        public ActionResult ChangeOfferStatus([FromBody] OfferStatusModel offerStatusModel)
        {
            var validate = _tokenService.ValidateClaims(Request, ClaimType.OFFER, offerStatusModel.OfferId);
            if (!validate) return Unauthorized();

            var authorId = _tokenService.GetAuthorId(Request);
            if (!authorId.HasValue) return BadRequest("User does not exist");

            bool res = _offerService.ChangeOfferStatus(offerStatusModel);
            return Ok(res);
        }

        [HttpGet, Authorize(Roles="ADMIN")]
        public ActionResult GetOffersAdmin()
        {
            List<OfferModel> res = _offerService.GetOffersAdmin();
            return Ok(res);

        }
    }
}
