using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChildCategoryController : ControllerBase
    {

        private readonly AppDbContext _context;

        public ChildCategoryController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetChildCategories")]
        public async Task<IActionResult> GetChildCategories([FromQuery] int? id, [FromQuery] int? subCategoryId, [FromQuery] string? name, [FromQuery] int? status)
        {
            

            // Start building the query
            var query =  _context.ChildCategories .AsQueryable();

            // Apply filters based on the provided query parameters
            if (id.HasValue)
                query = query.Where(c => c.Id == id.Value);

            if (subCategoryId.HasValue)
                query = query.Where(c => c.SubCategoryId == subCategoryId.Value);

            if (!string.IsNullOrEmpty(name))
                query = query.Where(c => c.Name.Contains(name));

            if (status.HasValue)
                query = query.Where(c => c.Status == status.Value);

            // Execute the query asynchronously
            var childCategories = await query.ToListAsync();

            if (childCategories == null || !childCategories.Any())
            {
                return NotFound(new { message = "No child categories found matching the criteria." });
            }

            return Ok(new { message = "Child categories retrieved successfully", data = childCategories });
        }

        [HttpPost]
        public async Task<IActionResult> CreateChildCategory([FromBody] childCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var childCategory = new ChildCategory
            {
                SubCategoryId = dto.SubCategoryId,
                Name = dto.Name,
                Slug = dto.Slug,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            await _context.ChildCategories.AddAsync(childCategory);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Child category created successfully", data = childCategory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the child category.", error = ex.Message });
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateChildCategory([FromQuery]int id, [FromBody] childCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var childCategory = await _context.ChildCategories.FindAsync(id);
            if (childCategory == null)
            {
                return NotFound(new { message = "Child category not found." });
            }

            childCategory.SubCategoryId = dto.SubCategoryId;
            childCategory.Name = dto.Name;
            childCategory.Slug = dto.Slug;
            childCategory.Status = dto.Status;
            childCategory.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Child category updated successfully", data = childCategory });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the child category.", error = ex.Message });
            }
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteChildCategory([FromQuery]int id)
        {
            var childCategory = await _context.ChildCategories.FindAsync(id);
            if (childCategory == null)
            {
                return NotFound(new { message = "Child category not found." });
            }

            _context.ChildCategories.Remove(childCategory);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Child category deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the child category.", error = ex.Message });
            }
        }


    }
}
