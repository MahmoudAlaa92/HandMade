global using Microsoft.IdentityModel.Tokens;
using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImageGalleriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductImageGalleriesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProduct([FromForm] ProductImageGalleryDto productImageGalleryDto)
        {
            if (productImageGalleryDto.Image == null || productImageGalleryDto.Image.Length == 0)
                return BadRequest("No image uploaded.");
            if (!ModelState.IsValid || productImageGalleryDto == null || productImageGalleryDto.ProductId <= 0) return BadRequest();
            var product = await _context.Products.FindAsync(productImageGalleryDto.ProductId);
            if (product == null) return BadRequest("This product is not found");

            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images/productsImgs");


            var filePath = Path.Combine(uploadsFolder, productImageGalleryDto.Image.FileName);


            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productImageGalleryDto.Image.CopyToAsync(stream);
            }

            var productImageGallery = new ProductImageGallery
            {
                ProductId = productImageGalleryDto.ProductId,
                CreatedAt = DateTime.Now,
                Image = ($"/images/{productImageGalleryDto.Image.FileName}")
            };

            await _context.ProductImageGalleries.AddAsync(productImageGallery);
            await _context.SaveChangesAsync();

            return Ok(productImageGallery);

        }



        [HttpGet("Product id")]
        public async Task<IActionResult> GetProduct([FromQuery] int prodId)//int id
        {
            var product = await _context.ProductImageGalleries.ToListAsync();




            product = await _context.ProductImageGalleries.Where(p => p.ProductId == prodId)
              .ToListAsync();

            if (product.IsNullOrEmpty())
                return NotFound("Product img not found.");

            foreach (var p in product)
            {
                if (!string.IsNullOrEmpty(p.Image))
                {
                    p.Image = $"{Request.Scheme}://{Request.Host}/{p.Image}";
                }
            }
            return Ok(product);
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetALLProduct()
        {
            var product = await _context.ProductImageGalleries.ToListAsync();

            if (product.IsNullOrEmpty())
                return NotFound("Product img not found.");

            foreach (var p in product)
            {
                if (!string.IsNullOrEmpty(p.Image))
                {
                    p.Image = $"{Request.Scheme}://{Request.Host}/{p.Image}";
                }
                else
                    p.Image = "Product img not found.";
            }
            return Ok(product);
        }


        [HttpDelete()]
        public async Task<IActionResult> deleteProductimg([FromQuery] int productid)
        {

            var product = await _context.ProductImageGalleries.Where(p => p.ProductId == productid)
                 .ToListAsync();

            if (product.IsNullOrEmpty())
                return NotFound("Product img not found.");



            _context.ProductImageGalleries.RemoveRange(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
