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
    public class productReviwGalaryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public productReviwGalaryController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        [HttpGet("all")]
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
          // product = await _context.ProductImageGalleries.ToListAsync();

           var   product = await _context.ProductReviewGalleries.Where(p => p.ProductReviewId == prodId)
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
        /* [HttpPost("upload")]
         public async Task<IActionResult> UploadProduct([FromForm] IFormFile image, [FromBody] string name)
         {
             if (image == null || image.Length == 0)
                 return BadRequest("No image uploaded.");

             // تحديد مسار التخزين داخل wwwroot/images
             var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images");
             if (!Directory.Exists(uploadsFolder))
                 Directory.CreateDirectory(uploadsFolder); // إنشاء المجلد إذا لم يكن موجودًا

          //   var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName); // إنشاء اسم فريد للصورة
             var filePath = Path.Combine(uploadsFolder, image.FileName);

             // حفظ الصورة على السيرفر
             using (var stream = new FileStream(filePath, FileMode.Create))
             {
                 await image.CopyToAsync(stream);
             }

             // تخزين المسار في قاعدة البيانات
             var product = new ProductReviewGallery
             {

               //  Image = Convert.ToByte($"/images/{image.FileName}")// مسار الصورة بالنسبة للتطبيق
             };

             // لنفترض أن عندك DbContext اسمه _context
             _context.ProductReviewGalleries.Add(product);
             await _context.SaveChangesAsync();

             return Ok();
         }*/


        [HttpPost("upload")]
        public async Task<IActionResult> UploadProduct(IFormFile image, int prodId,int revId)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "wwwroot/images/productsImgs");
            var filePath = Path.Combine(uploadsFolder, image.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }


            var product = new ProductReviewGallery
            {
                ProductReviewId = prodId,
                CreatedAt = DateTime.Now,
                Image = ($"/images/{image.FileName}")
            };

            await _context.ProductReviewGalleries.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(product);

        }



        [HttpDelete("id")]
        public async Task<IActionResult> deleteProductimg([FromQuery] int id)
        {
            //  product = await _context.ProductImageGalleries.ToListAsync();

            var product = await _context.ProductReviewGalleries.FindAsync( id);

            if (product==null)
                 return NotFound("Product img not found.");



            _context.ProductReviewGalleries.RemoveRange(product);
            await _context.SaveChangesAsync();

            return Ok(product);
        }
    }
}
