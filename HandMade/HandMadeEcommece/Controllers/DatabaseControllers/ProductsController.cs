using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductsController(AppDbContext _Context, IMapper mapper, IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            Context = _Context;
            _Mapper = mapper;
            _HttpContextAccessor = httpContextAccessor;
            this.webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductAll()
        {
            var Products = await Context.Products.ToListAsync();
            if (Products == null) return NotFound();
            return Ok(Products);
        }

        //[HttpGet]
        //public async Task<IActionResult> GetProduct([FromQuery] List<int> ids)
        //{
        //    if (ids == null) return BadRequest();
        //    var products = new List<Product>();
        //    foreach (var id in ids)
        //    {
        //        if (id <= 0) continue;
        //        var product = await Context.Products.FindAsync(id);
        //        if (product == null) continue;
        //        products.Add(product);
        //    }
        //    if (products.Count == 0) return BadRequest();
        //    return Ok(products);
        //}

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)// change fromForm to formbody
        {
            if (!ModelState.IsValid || productDto == null || productDto.VendorId <= 0 || productDto.CouponId < 0 || productDto.BrandId < 0 || productDto.CategoryId < 0) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(productDto.VendorId);
            if (vendor == null) return BadRequest("This vendor is not found");
            if (productDto == null) return BadRequest();
            if (await Context.Coupons.FindAsync(productDto.CouponId) == null)
            {
                productDto.CouponId = null;
            }

            if (await Context.Categories.FindAsync(productDto.CategoryId) == null)
            {
               productDto.CategoryId = null;
            }

            if (await Context.Brands.FindAsync(productDto.BrandId) == null)
            {
                productDto.BrandId = null;
            }
            var httpContext = _HttpContextAccessor.HttpContext;
            var product = new Product
            {
                Name = productDto.Name,
                LongDescription = productDto.LongDescription,
                OfferEndDate = productDto.OfferEndDate,
                OfferStartDate = productDto.OfferStartDate,
                BrandId = productDto.BrandId,
               CategoryId = productDto.CategoryId,
                ShortDescription = productDto.ShortDescription,
                SeoDescription = productDto.SeoDescription,
                CouponId = productDto.CouponId,
                IsApproved = productDto.IsApproved,
                SeoTitle = productDto.SeoTitle,
                Sku = productDto.Sku,
                OfferPrice = productDto.OfferPrice,
                Price = productDto.Price,
                ThumbImage = productDto.ThumbImage,
                Slug = productDto.Slug,
                Qty = productDto.Qty,
                VendorId = productDto.VendorId,
                ProductType = productDto.ProductType,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };
            await Context.Products.AddAsync(product);
            await Context.SaveChangesAsync();
            return Ok(product);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] ProductDto productDto)
        {
            if (!ModelState.IsValid || productDto == null || productDto.VendorId <= 0 || productDto.CouponId < 0 || productDto.BrandId < 0 || productDto.CategoryId < 0) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(productDto.VendorId);
            if (vendor == null) return BadRequest("This vendor is not found");
            if (productDto == null) return BadRequest();
            if (await Context.Coupons.FindAsync(productDto.CouponId) == null)
            {
                productDto.CouponId = null;
            }

            if (await Context.Categories.FindAsync(productDto.CategoryId) == null)
            {
                productDto.CategoryId = null;
            }

            if (await Context.Brands.FindAsync(productDto.BrandId) == null)
            {
                productDto.BrandId = null;
            }
            var product = await Context.Products.FindAsync(id);
            if (product == null) return NotFound();
            var httpContext = _HttpContextAccessor.HttpContext;
            product.ThumbImage = productDto.ThumbImage;
            product.UpdatedAt = DateTime.UtcNow;
            product.CreatedAt = productDto.CreatedAt;
            product = _Mapper.Map<Product>(productDto);
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (id <= 0) return BadRequest();
            var product = await Context.Products.FindAsync(id);
            if (product == null) return NotFound();
            Context.Products.Remove(product);
            await Context.SaveChangesAsync();
            return Ok(product);
        }
    }
}
