using AdvertisementPortal.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdvertisementPortal.Common.Models.DatabaseModels
{
    public class OfferModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public long ContactNumber { get; set; }
        public int CategoryId { get; set; }
        public virtual CategoryModel Category { get; set; }
        public int AuthorId { get; set; }
        public int Price { get; set; }
        public virtual UserModel Author { get; set; }
        public virtual IEnumerable<ImageDataModel> Images { get; set; }
        public bool IsActive { get; set; }
        public OfferStatus OfferStatus { get; set; }
    }
}
