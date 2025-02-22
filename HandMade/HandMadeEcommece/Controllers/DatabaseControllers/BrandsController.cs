using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandMadeEcommece.helper;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment _WebHostEnvironment;
        public BrandsController(AppDbContext _Context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            Context = _Context;
            _Mapper = mapper;
            _HttpContextAccessor = httpContextAccessor;
            _WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetBrandsAll()
        {
            var brands = await Context.Brands.ToListAsync();
            if (brands == null) return NotFound();
            return Ok(brands);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetBrand([FromQuery] List<int> ids)
        //{
        //    if (ids == null) return BadRequest();
        //    var brands = new List<Brand>();
        //    foreach (var id in ids)
        //    {
        //        if (id <= 0) continue;
        //        var brand = await Context.Brands.FindAsync(id);
        //        if (brand == null) continue;
        //        brands.Add(brand);
        //    }
        //    if (brands.Count == 0) return BadRequest();
        //    return Ok(brands);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromBody] BrandDto brandDto)
        {
            if (!ModelState.IsValid || brandDto == null) return BadRequest();
            if (!Enum.IsDefined(typeof(BrandStatus), brandDto.Status))
            {
                return BadRequest("Invalid brand status.");
            }
            var httpContext = _HttpContextAccessor.HttpContext;
            var brand = new Brand
            {
                Logo = brandDto.Logo,
                Name = brandDto.Name,
                Slug = brandDto.Slug,
                Status = brandDto.Status.ToString(),
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            await Context.Brands.AddAsync(brand);
            await Context.SaveChangesAsync();
            return Ok(brand);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateBrand(int id, BrandDto brandDto)
        {
            if (id <= 0 || brandDto == null) return BadRequest();
            if (!Enum.IsDefined(typeof(BrandStatus), brandDto.Status))
            {
                return BadRequest("Invalid brand status.");
            }
            var brand = await Context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            var httpContext = _HttpContextAccessor.HttpContext;
            brand.Name = brandDto.Name;
            brand.UpdatedAt = DateTime.UtcNow;
            brand.CreatedAt = brandDto.CreatedAt;
            brand.Status = brandDto.Status.ToString();
            brand.Logo = brandDto.Logo;
            brand.Slug = brandDto.Slug;
            Context.Brands.Update(brand);
            await Context.SaveChangesAsync();
            return Ok(brand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            if (id <= 0) return BadRequest();
           
            var brand = await Context.Brands.FindAsync(id);
            if (brand == null) return NotFound("This brand not found.");
            Context.Brands.Remove(brand);
            await Context.SaveChangesAsync();
            return Ok(brand);
        }
    }
}
