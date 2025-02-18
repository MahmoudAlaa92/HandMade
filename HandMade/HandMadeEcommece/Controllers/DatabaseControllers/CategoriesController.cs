using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CategoriesController(AppDbContext _Context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            Context = _Context;
            _Mapper = mapper;
            _HttpContextAccessor = httpContextAccessor;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategoriesAll()
        {
            var Categories = await Context.Categories.ToListAsync();
            if (Categories == null) return NotFound();
            return Ok(Categories);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetCategory([FromQuery] List<int> ids)
        //{
        //    if (ids == null) return BadRequest();
        //    var categories = new List<Category>();
        //    foreach (var id in ids)
        //    {
        //        if (id <= 0) continue;
        //        var category = await Context.Categories.FindAsync(id);
        //        if (category == null) continue;
        //        categories.Add(category);
        //    }
        //    if (categories.Count == 0) return BadRequest();
        //    return Ok(categories);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromForm] CategoryDto categoryDto)
        {
            if (categoryDto == null) return BadRequest();
            var httpContext = _HttpContextAccessor.HttpContext;
            var category = new Category
            {
                Icon = await Methods.GetImagesFromPath(categoryDto.Icon,"Categories",httpContext,webHostEnvironment),
                Name = categoryDto.Name,
                Slug = categoryDto.Slug,
                Status = 1,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            await Context.Categories.AddAsync(category);
            await Context.SaveChangesAsync();
            return Ok(category);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (id <= 0 || categoryDto == null) return BadRequest();
            var category = await Context.Categories.FindAsync(id);
            if (category == null) return NotFound();
            var httpContext = _HttpContextAccessor.HttpContext;
            category.Name = categoryDto.Name;
            category.UpdatedAt = DateTime.UtcNow;
            category.CreatedAt = categoryDto.CreatedAt;
            category.Status = categoryDto.Status;
            category.Icon = await Methods.GetImagesFromPath(categoryDto.Icon,"Categories",httpContext,webHostEnvironment);
            Context.Categories.Update(category);
            await Context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var categories = new List<Category>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var category = await Context.Categories.FindAsync(id);
                if (category == null) continue;
                Context.Categories.Remove(category);
                categories.Add(category);
            }
            await Context.SaveChangesAsync();
            if (categories.Count == 0) return BadRequest();
            return Ok(categories);
        }
    }
}
