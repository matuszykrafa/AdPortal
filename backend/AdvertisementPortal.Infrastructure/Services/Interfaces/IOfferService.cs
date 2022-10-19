using AdvertisementPortal.Common.Models;
using AdvertisementPortal.Common.Models.DatabaseModels;

namespace AdvertisementPortal.Infrastructure.Services.Interfaces
{
    public interface IOfferService
    {
        OfferModel? GetOfferById(int offerId, bool isAdmin);
        List<OfferModel> GetOffersForUser(int authorId);
        OfferModel? GetOfferManagementById(int offerId);
        List<OfferModel> GetOffers();
        OfferModel AddOrUpdateOffer(int authorId, OfferModel offerModel);
        bool ChangeOfferStatus(OfferStatusModel offerStatusModel);
        List<OfferModel> GetOffersAdmin();
        List<OfferModel> GetOffersByCategory(int categoryId);
    }
}
