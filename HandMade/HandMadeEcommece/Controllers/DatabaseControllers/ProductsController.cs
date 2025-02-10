using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        public ProductsController(AppDbContext _Context, IMapper mapper)
        {
            Context = _Context;
            _Mapper = mapper;
        }

        [HttpGet("GetProductAll")]
        public async Task<IActionResult> GetProductAll()
        {
            var Products = await Context.Products.ToListAsync();
            if (Products == null) return NotFound();
            return Ok(Products);
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var products = new List<Product>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var product = await Context.Products.FindAsync(id);
                if (product == null) continue;
                products.Add(product);
            }
            if (products.Count == 0) return BadRequest();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)// change fromForm to formbody
        {
            if (!ModelState.IsValid || productDto == null||productDto.VendorId <= 0) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(productDto.VendorId);
            if (vendor == null) return BadRequest("This vendor is not found");
            if (productDto == null) return BadRequest();
            if (productDto.CouponId.HasValue)
            {
                var coupon  = await Context.Coupons.FindAsync(productDto.CouponId);
                if (coupon == null) return BadRequest("This coupon is not found");
            }

            if (productDto.ChildCategoryId.HasValue)
            {
                var child = await Context.ChildCategories.FindAsync(productDto.ChildCategoryId);
                if (child == null) return BadRequest("This ChildCategory is not found");
            }

            if (productDto.BrandId.HasValue)
            {
                var brand = await Context.Brands.FindAsync(productDto.BrandId);
                if (brand == null) return BadRequest("This brand is not found");
            }
            var product = new Product
            {
                Name = productDto.Name,
                LongDescription = productDto.LongDescription,
                OfferEndDate = productDto.OfferEndDate,
                OfferStartDate = productDto.OfferStartDate,
                BrandId = productDto.BrandId,
                ChildCategoryId = productDto.ChildCategoryId,
                ShortDescription = productDto.ShortDescription,
                SeoDescription = productDto.SeoDescription,
                CouponId = productDto.CouponId,
                IsApproved = productDto.IsApproved,
                SeoTitle = productDto.SeoTitle,
                Sku = productDto.Sku,
                OfferPrice = productDto.OfferPrice,
                Price = productDto.Price,
                ThumbImage = await Methods.TransferImage(productDto.ThumbImage),
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
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductDto productDto)
        {
            if (!ModelState.IsValid || productDto == null || productDto.VendorId <= 0) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(productDto.VendorId);
            if (vendor == null) return BadRequest("This vendor is not found");
            if (productDto == null) return BadRequest();
            if (productDto.CouponId.HasValue)
            {
                var coupon = await Context.Coupons.FindAsync(productDto.CouponId);
                if (coupon == null) return BadRequest("This coupon is not found");
            }

            if (productDto.ChildCategoryId.HasValue)
            {
                var child = await Context.ChildCategories.FindAsync(productDto.ChildCategoryId);
                if (child == null) return BadRequest("This ChildCategory is not found");
            }

            if (productDto.BrandId.HasValue)
            {
                var brand = await Context.Brands.FindAsync(productDto.BrandId);
                if (brand == null) return BadRequest("This brand is not found");
            }
            var product = await Context.Products.FindAsync(id);
            if (product == null) return NotFound();
            product.ThumbImage = await Methods.TransferImage(productDto.ThumbImage);
            product.UpdatedAt = DateTime.UtcNow;
            product.CreatedAt = productDto.CreatedAt;
            product = _Mapper.Map<Product>(productDto);
            Context.Products.Update(product);
            await Context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] List<int> ids)
        {
            if (ids == null) return BadRequest();
            var products = new List<Product>();
            foreach (var id in ids)
            {
                if (id <= 0) continue;
                var product = await Context.Products.FindAsync(id);
                if (product == null) continue;
                Context.Products.Remove(product);
                products.Add(product);
            }
            await Context.SaveChangesAsync();
            if (products.Count == 0) return BadRequest();
            return Ok(products);
        }
    }
}
