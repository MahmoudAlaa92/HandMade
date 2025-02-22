using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductReviewsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductReviewsController(AppDbContext context)
        {
            _context = context;
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
        public async Task<IActionResult> AddproductRev([FromBody]ProductReviewDto dto)

        {

            if (dto != null)
            {
                if (dto.ProductId <= 0 || dto.UserId <= 0||await _context.Users.FindAsync(dto.UserId) == null || await _context.Products.FindAsync(dto.ProductId) == null) return BadRequest("Error in ProductId Or UserId.");
                var newrev = new ProductReview
                {
                    ProductId = dto.ProductId,
                    UserId = dto.UserId,
                    Review = dto.Review,
                    Rating = dto.Rating,
                    Status = 1,
                    CreatedAt = DateTime.UtcNow,
                };

                await _context.ProductReviews.AddAsync(newrev);
                await _context.SaveChangesAsync();

                return Ok();
            }

           return BadRequest();

        }


        [HttpPut]
        public async Task<IActionResult>UpdateProductReview(int id,  [FromBody]ProductReviewDto dto)
        {
            if (dto.ProductId <= 0 || dto.UserId <= 0 || await _context.Users.FindAsync(dto.UserId) == null || await _context.Products.FindAsync(dto.ProductId) == null) return BadRequest("Error in ProductId Or UserId.");
            var productReview = await _context.ProductReviews.FindAsync(dto.ProductId);
            if (productReview == null) return NotFound("This productReview is not found.");
            productReview.ProductId = dto.ProductId;
            productReview.UserId = dto.UserId;
            productReview.Review = dto.Review;
            productReview.Status = dto.Status;
            productReview.UpdatedAt = DateTime.UtcNow;
            productReview.Rating = dto.Rating;
             _context.ProductReviews.Update(productReview);
            await _context.SaveChangesAsync();
            return Ok(productReview);
        }


        [HttpDelete]
        public async Task<IActionResult> deletRev([FromQuery] int id)
        {
            var rev = await _context.ProductReviews.FindAsync(id);
            if (rev == null)
                return NotFound("rev not found");

            _context.ProductReviews.Remove(rev);
            await _context.SaveChangesAsync();
            return Ok(new { Message = "reiew deleted successfully", Deleteditem = rev });
        }
    }
}
