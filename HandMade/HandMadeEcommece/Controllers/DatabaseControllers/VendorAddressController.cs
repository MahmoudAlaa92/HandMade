using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        public VendorAddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVendorAddresses()
        {
            var userAddresses = await _context.VendorAddresses.ToListAsync();


            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { message = "No user addresses found." });
            }

            return Ok(userAddresses);
        }




        [HttpPost]
        public async Task<IActionResult> CreateVendorAddress([FromBody] VendorAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ua = new VendorAddress
            {
                VendorId = userAddress.VendorId,
                Country = userAddress.Country,
                State = userAddress.State,
                City = userAddress.City,
                Zip = userAddress.Zip,
                Address = userAddress.Address,
                CreatedAt = DateTime.UtcNow,

            };

            await _context.VendorAddresses.AddAsync(ua);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address created successfully", userAddress });
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteVendorAddress([FromQuery] int id)
        {
            var userAddress = await _context.VendorAddresses.FirstOrDefaultAsync(ua => ua.Id == id);
            if (userAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }
            _context.VendorAddresses.Remove(userAddress);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address deleted successfully" });
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVendorAddress(int id, [FromBody] VendorAddress userAddressDto)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserAddress = await _context.VendorAddresses.FindAsync(id);
            if (existingUserAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }


            existingUserAddress.VendorId = userAddressDto.VendorId;
            existingUserAddress.Country = userAddressDto.Country;
            existingUserAddress.State = userAddressDto.State;
            existingUserAddress.City = userAddressDto.City;
            existingUserAddress.Zip = userAddressDto.Zip;
            existingUserAddress.Address = userAddressDto.Address;
            existingUserAddress.UpdatedAt = DateTime.UtcNow;

            _context.VendorAddresses.Update(existingUserAddress);
            await _context.SaveChangesAsync();

            return Ok(existingUserAddress);
        }
    }
}
