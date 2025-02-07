using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductReviewController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetproductRev(
            [FromQuery] int prodid, [FromQuery] int? userid, [FromQuery] string? rating, [FromQuery] DateTime? created_at)
        {
            var productrev = _context.ProductReviews.
                Where(p => p.ProductId == prodid);//&& p.UserId == userid);

            
            if (productrev == null || !productrev.Any())
                return NotFound($"No reviws of productid{prodid} and userid {userid} is found :");

            if(userid>0)
                productrev = _context.ProductReviews.
                Where(p => p.UserId == userid);
            if (created_at.HasValue)
                productrev = productrev.Where(p => p.CreatedAt == created_at);

            if (!rating.IsNullOrEmpty())
                productrev = productrev.Where(p => p.Rating.ToLower().Contains(rating.ToLower()));

            var result = await productrev.ToListAsync();
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRev()
        {
            var rev = await _context.ProductReviews.ToListAsync();
            if (rev.IsNullOrEmpty())
                return NotFound($"No reviews is found :");
            return Ok(rev);
        }

        [HttpPost]
        public async Task<IActionResult> AddproductRev([FromBody] ProductRevDTO dto)

        {
            ;

            if (dto != null)
            {
                var newrev = new ProductReview
                {
                    ProductId = dto.ProductId,
                    UserId = dto.UserId,
                    Review = dto.Review,
                    Rating = dto.Rating,
                    Status = dto.Status,
                    CreatedAt = DateTime.UtcNow,
                };


                await _context.ProductReviews.AddAsync(newrev);
                await _context.SaveChangesAsync();

                return Ok();
            }


            return BadRequest();

        }

        
        [HttpDelete]
        public async Task<IActionResult> deletRev([FromQuery] int id, [FromQuery] int productid, [FromQuery] int userid)
        {
            var rev =  await _context.ProductReviews.FirstOrDefaultAsync(r => r.Id == id && r.ProductId == productid && r.UserId == userid);
            if (rev == null)
                return NotFound("rev not found");

            _context.ProductReviews.Remove(rev);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "reiew deleted successfully", Deleteditem = rev });
        }


    }
}
