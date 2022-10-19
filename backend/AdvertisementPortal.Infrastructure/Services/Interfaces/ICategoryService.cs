using AdvertisementPortal.Common.Models.DatabaseModels;

namespace AdvertisementPortal.Infrastructure.Services.Interfaces
{
    public interface ICategoryService
    {
        List<CategoryModel> GetCategories();
        List<CategoryModel> GetActiveCategories();
    }
}
