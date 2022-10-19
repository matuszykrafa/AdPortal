using AdvertisementPortal.Common.Enums;
using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementPortal.Infrastructure.Services.Implementations
{
    public sealed class OfferService : IOfferService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OfferService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public OfferModel? GetOfferById(int offerId, bool isAdmin)
        {
            var offer = _applicationDbContext.Offers
                .AsNoTracking()
                .Where(x => x.Id == offerId);

            if(!isAdmin)
                offer = offer
                    .Where(x => x.OfferStatus > Common.Enums.OfferStatus.PENDING || x.OfferStatus != Common.Enums.OfferStatus.REJECTED)
                    .Where(x => x.IsActive);
            return offer.FirstOrDefault(); ;
        }

        public List<OfferModel> GetOffersForUser(int authorId)
        {
            var offerList = _applicationDbContext.Offers
                .Where(x => x.AuthorId == authorId)
                .AsNoTracking()
                .ToList();
            return offerList;
        }

        public OfferModel? GetOfferManagementById(int offerId)
        {
            var offer = _applicationDbContext.Offers
                .Where(x => x.Id == offerId)
                .AsNoTracking()
                .FirstOrDefault();
            return offer;
        }

        public List<OfferModel> GetOffers()
        {
            var offers = _applicationDbContext.Offers
                .Where(x => x.OfferStatus == Common.Enums.OfferStatus.ACCEPTED)

                .AsNoTracking().ToList();
            return offers;
        }

        public List<OfferModel> GetOffersByCategory(int categoryId)
        {
            var offers = _applicationDbContext.Offers
                .Where(x => x.OfferStatus == Common.Enums.OfferStatus.ACCEPTED)
                .Where(x => x.CategoryId == categoryId)
                .AsNoTracking().ToList();
            return offers;
        }

        public OfferModel AddOrUpdateOffer(int authorId, OfferModel offerModel)
        {
            var updateOfferModel = new OfferModel() { 
                AuthorId = authorId, IsActive = true
            };
            if (offerModel.Id > 0)
            {
                var oldOfferModel = _applicationDbContext.Offers
                .FirstOrDefault(x => x.Id == offerModel.Id);
                if (oldOfferModel is not null)
                    updateOfferModel = oldOfferModel;
            }

            updateOfferModel.Title = offerModel.Title;
            updateOfferModel.Description = offerModel.Description;
            updateOfferModel.ContactNumber = offerModel.ContactNumber;
            updateOfferModel.CategoryId = offerModel.CategoryId;
            updateOfferModel.IsActive = offerModel.IsActive;
            updateOfferModel.OfferStatus = Common.Enums.OfferStatus.PENDING;
            _applicationDbContext.Update(updateOfferModel);
            _applicationDbContext.SaveChanges();
            updateOfferModel.Author = null;
            updateOfferModel.Images = null;
            return updateOfferModel;

        }

        public bool ChangeOfferStatus(OfferStatusModel offerStatusModel)
        {
            var offer = _applicationDbContext.Offers.Where(x => x.Id == offerStatusModel.OfferId).FirstOrDefault();
            if (offer is null) return false;
            offer.OfferStatus = offerStatusModel.OfferStatus;
            _applicationDbContext.SaveChanges();
            return true;
        }

        public List<OfferModel> GetOffersAdmin()
        {
            var offers = _applicationDbContext.Offers
                .AsNoTracking().ToList();
            return offers;
        }
    }
}
