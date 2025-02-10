using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AdminAddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAdminAddresses()
        {
            var userAddresses = await _context.AdminAddresses.ToListAsync();


            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { message = "No user addresses found." });
            }

            return Ok(userAddresses);
        }




        [HttpPost]
        public async Task<IActionResult> CreateAdminAddress([FromBody] AdminAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ua = new AdminAddress
            {
                AdminId = userAddress.AdminId,
                Country = userAddress.Country,
                State = userAddress.State,
                City = userAddress.City,
                Zip = userAddress.Zip,
                Address = userAddress.Address,
                CreatedAt = DateTime.UtcNow,

            };

            await _context.AdminAddresses.AddAsync(ua);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address created successfully", userAddress });
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteAdminAddress([FromQuery] int id)
        {
            var userAddress = await _context.AdminAddresses.FirstOrDefaultAsync(ua => ua.Id == id);
            if (userAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }
            _context.AdminAddresses.Remove(userAddress);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address deleted successfully" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminAddress(int id, [FromBody] AdminAddress userAddressDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserAddress = await _context.AdminAddresses.FindAsync(id);
            if (existingUserAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }


            existingUserAddress.AdminId = userAddressDto.AdminId;
            existingUserAddress.Country = userAddressDto.Country;
            existingUserAddress.State = userAddressDto.State;
            existingUserAddress.City = userAddressDto.City;
            existingUserAddress.Zip = userAddressDto.Zip;
            existingUserAddress.Address = userAddressDto.Address;
            existingUserAddress.UpdatedAt = DateTime.UtcNow;

            _context.AdminAddresses.Update(existingUserAddress);
            await _context.SaveChangesAsync();

            return Ok(existingUserAddress);
        }
    }
}
