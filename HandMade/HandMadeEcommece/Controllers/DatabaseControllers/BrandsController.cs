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
        public BrandsController(AppDbContext _Context, IMapper mapper)
        {
            Context = _Context;
            _Mapper = mapper;
        }

        [HttpGet("GetBrandsAll")]
        public async Task<IActionResult> GetBrandsAll()
        {
            var brands = await Context.Brands.ToListAsync();
            if (brands == null) return NotFound();
            return Ok(brands);
        }

        [HttpGet]
        public async Task<IActionResult> GetBrand([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var brands = new List<Brand>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var brand = await Context.Brands.FindAsync(id);
                if (brand == null) continue;
                brands.Add(brand);
            }
            if (brands.Count == 0) return BadRequest();
            return Ok(brands);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBrand([FromForm] BrandDto brandDto)
        {
            if (!ModelState.IsValid || brandDto == null) return BadRequest();
            if (!Enum.IsDefined(typeof(BrandStatus), brandDto.Status))
            {
                return BadRequest("Invalid brand status.");
            }
            var brand = new Brand
            {
                Logo = await Methods.TransferImage(brandDto.Logo),
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
            brand.Name = brandDto.Name;
            brand.UpdatedAt = DateTime.UtcNow;
            brand.CreatedAt = brandDto.CreatedAt;
            brand.Status = brandDto.Status.ToString();
            brand.Logo = await Methods.TransferImage(brandDto.Logo);
            Context.Brands.Update(brand);
            await Context.SaveChangesAsync();
            return Ok(brand);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBrand([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var brands = new List<Brand>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var brand = await Context.Brands.FindAsync(id);
                if (brand == null) continue;
                Context.Brands.Remove(brand);
                brands.Add(brand);
            }
            await Context.SaveChangesAsync();
            if (brands.Count == 0) return BadRequest();
            return Ok(brands);
        }
    }
}
