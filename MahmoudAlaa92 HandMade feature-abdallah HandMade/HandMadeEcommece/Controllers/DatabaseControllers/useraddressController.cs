using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class useraddressController : ControllerBase
    {
        private readonly AppDbContext _context;

        public useraddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAddresses()
        {
            var userAddresses = await _context.UserAddresses.ToListAsync();
               
                

            // Check if any records were found
            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { message = "No user addresses found." });
            }

            return Ok(userAddresses);
        }


        [HttpGet("filter")]
        public async Task<IActionResult> GetUserAddresses([FromQuery] int? userId, [FromQuery] int? vendorId, [FromQuery] string? country)
        {
            var query = _context.UserAddresses.AsQueryable(); // Start with the base query

            // Filter by userId if provided
            if (userId.HasValue)
            {
                query = query.Where(ua => ua.UserId == userId.Value);
            }

            // Filter by vendorId if provided
            if (vendorId.HasValue)
            {
                query = query.Where(ua => ua.VendorId == vendorId.Value);
            }

            // Filter by country if provided
            if (!string.IsNullOrEmpty(country))
            {
                query = query.Where(ua => ua.Country.Contains(country));
            }

            // Include related entities (User, Vendor, Admin)
            var userAddresses = await query.ToListAsync();

            // Check if any records were found
            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { message = "No user addresses found matching the given criteria." });
            }

            return Ok(userAddresses);
        }


        [HttpPost]
        public async Task<IActionResult> CreateUserAddress([FromBody] useradressDto userAddress)
        {
            if (!ModelState.IsValid)
            {
                // Return a BadRequest if the DTO is invalid
                return BadRequest(ModelState);
            }

            var ua = new UserAddress
            {
                UserId = userAddress.UserId,
                VendorId = userAddress.VendorId,
                AdminId = userAddress.AdminId,
                Country = userAddress.Country,
                State = userAddress.State,
                City = userAddress.City,
                Zip = userAddress.Zip,
                Address = userAddress.Address,
                CreatedAt = DateTime.UtcNow,
                
            };

                 // Add the new user address
                await _context.UserAddresses.AddAsync(ua);
                // Save the changes to the database asynchronously
                await _context.SaveChangesAsync();
                return Ok(new { message = "User address created successfully", userAddress });
           
            
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteUserAddress([FromQuery]int id)
        {
           
            //try
            //{

              
            var userAddress = await _context.UserAddresses.FirstOrDefaultAsync(ua=>ua.Id==id);
            if (userAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }
                _context.UserAddresses.Remove(userAddress);         
                await _context.SaveChangesAsync();
                return Ok(new { message = "User address deleted successfully" });



            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, new { message = "An error occurred while deleting the user address.", error = ex.Message });
            //}
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAddress(int id, [FromBody] useradressDto userAddressDto)
        {
            // Validate the model
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Find the existing user address by ID
            var existingUserAddress = await _context.UserAddresses.FindAsync(id);
            if (existingUserAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }

            // Map the updated fields from the DTO to the existing user address
            existingUserAddress.UserId = userAddressDto.UserId;
            existingUserAddress.VendorId = userAddressDto.VendorId;
            existingUserAddress.AdminId = userAddressDto.AdminId;
            existingUserAddress.Country = userAddressDto.Country;
            existingUserAddress.State = userAddressDto.State;
            existingUserAddress.City = userAddressDto.City;
            existingUserAddress.Zip = userAddressDto.Zip;
            existingUserAddress.Address = userAddressDto.Address;
            existingUserAddress.UpdatedAt = DateTime.UtcNow;

            try
            {
                // Save the updated user address asynchronously
                await _context.SaveChangesAsync();
                return Ok(new { message = "User address updated successfully", userAddress = existingUserAddress });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the user address.", error = ex.Message });
            }
        }

    }
}

