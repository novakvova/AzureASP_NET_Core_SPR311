using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebWorker.Interfaces;
using WebWorker.Models.Category;

namespace WebWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController(ICategoryService categoryService) 
        : ControllerBase
    {

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] CategoryCreateModel model)
        {
            var categoryId = await categoryService.CreateAsync(model);
            return Ok(new { categoryId });
        }

    }
}
