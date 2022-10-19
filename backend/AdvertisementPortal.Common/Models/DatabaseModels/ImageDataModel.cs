namespace AdvertisementPortal.Common.Models.DatabaseModels
{
    public class ImageDataModel
    {
        public int Id { get; set; }
        public byte[] Data { get; set; }
        public int OfferId { get; set; }
        public virtual OfferModel Offer { get; set; }
    }
}
