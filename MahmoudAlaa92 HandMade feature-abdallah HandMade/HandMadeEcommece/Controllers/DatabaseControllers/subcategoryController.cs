using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
//using System.Linq;
//using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class subcategoryController : ControllerBase
    {
        private readonly AppDbContext _context;

        public subcategoryController(AppDbContext context)
        {
            _context = context;
        }

       
        [HttpGet]
        public async Task<IActionResult> GetAllsubCategory()
        {
            var subcat = await _context.SubCategories.ToListAsync();
            if (subcat.IsNullOrEmpty() )
                return NotFound($"No subcategory is found :");

            return Ok(subcat);
        }

        [HttpGet("search")]
        public async Task<IActionResult> GetSubCat([FromQuery]int id, [FromQuery] string name, [FromQuery] string? slug, [FromQuery] int? status)
        {
           
            var query = _context.SubCategories.Where(s=>s.Id==id && s.Name.ToLower().Contains(name.ToLower()));
            

            //if (id.HasValue)
            //    query = query.Where(sub => sub.Id == id.Value);

            //if (!string.IsNullOrEmpty(name))
            //    query = query.Where(sub => sub.Name.ToLower().Contains(name.ToLower()));

            if (!string.IsNullOrEmpty(slug))
                query = query.Where(sub => sub.Slug.ToLower().Contains(slug.ToLower()));

            if (status.HasValue)
                query = query.Where(sub => sub.Status == status.Value);

            var result = await query.ToListAsync();

            if (result.Any())
                return Ok(result);

            return NotFound("No subcategories found matching the criteria.");

        }

        //[HttpPost]
        //public async Task<IActionResult> AddNewsubcat([FromBody]Subcategory_DTO dto)
        //{
        //    if (dto!= null)
        //    {
        //        var subcat = new SubCategory
        //        {

        //            CategoryId = dto.CategoryId,
        //            Name = dto.Name,
        //            Slug = dto.Slug,
        //            Status = dto.Status,
        //            CreatedAt = dto.CreatedAt,


        //        };
        //    await _context.SubCategories.AddAsync(subcat);
        //    _context.SaveChangesAsync();
        //    return Created("https://localhost:7016/api/subcategory/"+subcat.Id,subcat);
        //    }
        //    return BadRequest(ModelState);

        //}

        [HttpPost]
        public async Task<IActionResult> AddNewsubcat([FromBody] Subcategory_DTO dto)
        {
            if (dto != null)
            {
                
                var subcat = new SubCategory
                {
                    CategoryId = dto.CategoryId,
                    Name = dto.Name,
                    Slug = dto.Slug,
                    Status = dto.Status,

                    CreatedAt = dto.CreatedAt,
                };

                // Add the new subcategory and save changes asynchronously
                await _context.SubCategories.AddAsync(subcat);
                 _context.SaveChanges();

                return Created("https://localhost:7016/api/subcategory/" + subcat.Id, subcat);
            }

            // If dto is null or not valid, return BadRequest with validation errors
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditesubCat(int id, [FromBody] Subcategory_DTO dto)
        {
            var cat = await _context.SubCategories.FindAsync(id);
            if (cat == null)
                return NotFound($"No subcategory found with ID: {id}");
            cat.CategoryId = dto.CategoryId;
            cat.Name = dto.Name;
            cat.Slug = dto.Slug;
            cat.Status = dto.Status;
            cat.UpdatedAt = DateTime.Now;
            _context.SaveChanges();
            return Ok(cat);
        }

        [HttpDelete]
        public async Task<IActionResult> DeteleSubcategory([FromQuery] int? id, [FromQuery] string? name, [FromQuery] string? slug, [FromQuery] int? status)
        {

            var query = _context.SubCategories.AsQueryable();
            if (id.HasValue)
                query = query.Where(sub => sub.Id == id.Value);
            if (!string.IsNullOrEmpty(name))
                query = query.Where(sub => sub.Name.ToLower().Contains(name.ToLower()));
            if (!string.IsNullOrEmpty(slug))
                query = query.Where(sub => sub.Slug.ToLower().Contains(slug.ToLower()));
            if (status.HasValue)
                query = query.Where(sub => sub.Status == status.Value);

            var result = await query.ToListAsync();


             if (result == null)
                return NotFound("No Subcat found ");
            
                _context.SubCategories.RemoveRange(result);
                _context.SaveChanges();
             
            return Ok(new { Message = "subcategory deleted successfully", DeletedBrand = result });


        }

    }
}
