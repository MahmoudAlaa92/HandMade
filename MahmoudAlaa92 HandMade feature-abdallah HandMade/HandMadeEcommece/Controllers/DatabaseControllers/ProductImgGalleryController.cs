using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImgGalleryController : ControllerBase
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductImgGalleryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProduct(IFormFile image, int prodId)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images/productsImgs");
            

            var filePath = Path.Combine(uploadsFolder, image.FileName);

            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            var product = new productReviwGalary
            {
                ProductId = prodId,
                CreatedAt = DateTime.Now,
                Image = ($"/images/{image.FileName}")
            };

         await   _context.ProductImageGalleries.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);

        }



        [HttpGet("Product id")]
        public async Task<IActionResult> GetProduct([FromQuery] int prodId)//int id
        {
            var product = await _context.ProductImageGalleries.ToListAsync();


            

              product =await   _context.ProductImageGalleries.Where(p => p.ProductId == prodId)
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


        [HttpGet("all")]
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


        [HttpDelete("id")]
        public async Task<IActionResult> deleteProductimg([FromQuery] int productid)
        {
           //  product = await _context.ProductImageGalleries.ToListAsync();

         var  product = await _context.ProductImageGalleries.Where(p => p.ProductId == productid)
              .ToListAsync();

            if (product.IsNullOrEmpty())
                return NotFound("Product img not found.");

           
            
         _context.ProductImageGalleries.RemoveRange(product);
            await _context.SaveChangesAsync();

            return Ok(product);
    }
    }
}

