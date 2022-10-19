using AdvertisementPortal.Common.Models.DatabaseModels;
using AdvertisementPortal.DatabaseAccess;
using AdvertisementPortal.Infrastructure.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvertisementPortal.Infrastructure.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CategoryService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public List<CategoryModel> GetCategories()
        {
            var categories = _applicationDbContext.Categories.ToList();
            return categories;
        }
        public List<CategoryModel> GetActiveCategories()
        {
            var categories = _applicationDbContext.Categories.Where(x => x.IsActive).ToList();
            return categories;
        }
    }
}
