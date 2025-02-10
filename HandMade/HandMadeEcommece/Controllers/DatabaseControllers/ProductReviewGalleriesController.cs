using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HandMadeEcommece.Models.Dto;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewGalleriesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductReviewGalleriesController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("All")]
        public async Task<IActionResult> GetALL()
        {
            var product = await _context.ProductReviewGalleries.ToListAsync();

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


        [HttpGet("Product id")]
        public async Task<IActionResult> GetRevGallary([FromQuery] int prodId)//int id
        {

            var product = await _context.ProductReviewGalleries.Where(p => p.ProductReviewId == prodId)
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
        


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProduct([FromForm]ProductReviewGalleryDto productReviewGalleryDto)
        {

            if (productReviewGalleryDto.Image == null || productReviewGalleryDto.Image.Length == 0)
                return BadRequest("No image uploaded.");

            if (!ModelState.IsValid || productReviewGalleryDto == null || productReviewGalleryDto.ProductReviewId <= 0) return BadRequest();
            var productReview  = await _context.ProductReviews.FindAsync(productReviewGalleryDto.ProductReviewId);
            if (productReview == null) return BadRequest("This productReview is not found");
            var product = await _context.Products.FindAsync(productReview.ProductId);
            if (product == null) return BadRequest("This product is not found");

            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images/productsImgs");
            var filePath = Path.Combine(uploadsFolder, productReviewGalleryDto.Image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productReviewGalleryDto.Image.CopyToAsync(stream);
            }


            var productReviewGallery = new ProductReviewGallery
            {
                ProductReviewId = productReviewGalleryDto.ProductReviewId,
                CreatedAt = DateTime.Now,
                Image = ($"/images/{productReviewGalleryDto.Image.FileName}")
            };

            await _context.ProductReviewGalleries.AddAsync(productReviewGallery);
            await _context.SaveChangesAsync();

            return Ok(productReviewGallery);

        }



        [HttpDelete("id")]
        public async Task<IActionResult> deleteProductimg([FromQuery] int id)
        {
     

            var product = await _context.ProductReviewGalleries.FindAsync(id);

            if (product == null)
                return NotFound("Product img not found.");



            _context.ProductReviewGalleries.RemoveRange(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
