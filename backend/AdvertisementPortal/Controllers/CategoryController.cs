using AdvertisementPortal.Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementPortal.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController, AllowAnonymous]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult GetCategories()
        {
            var res = _categoryService.GetCategories();
            return Ok(res);
        }

        [HttpGet]
        public IActionResult GetActiveCategories()
        {
            var res = _categoryService.GetActiveCategories();
            return Ok(res);
        }
    }
}
