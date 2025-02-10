using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using HandMadeEcommece.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAddressController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UserAddressController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserAddresses()
        {
            var userAddresses = await _context.UserAddresses.ToListAsync();


            if (userAddresses == null || !userAddresses.Any())
            {
                return NotFound(new { message = "No user addresses found." });
            }

            return Ok(userAddresses);
        }




        [HttpPost]
        public async Task<IActionResult> CreateUserAddress([FromBody] UserAddress userAddress)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ua = new UserAddress
            {
                UserId = userAddress.UserId,
                Country = userAddress.Country,
                State = userAddress.State,
                City = userAddress.City,
                Zip = userAddress.Zip,
                Address = userAddress.Address,
                CreatedAt = DateTime.UtcNow,

            };

            await _context.UserAddresses.AddAsync(ua);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address created successfully", userAddress });
        }



        [HttpDelete]
        public async Task<IActionResult> DeleteUserAddress([FromQuery] int id)
        {
            var userAddress = await _context.UserAddresses.FirstOrDefaultAsync(ua => ua.Id == id);
            if (userAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }
            _context.UserAddresses.Remove(userAddress);
            await _context.SaveChangesAsync();
            return Ok(new { message = "User address deleted successfully" });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserAddress(int id, [FromBody] UserAddress userAddressDto)
        {
           
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUserAddress = await _context.UserAddresses.FindAsync(id);
            if (existingUserAddress == null)
            {
                return NotFound(new { message = "User address not found." });
            }

  
            existingUserAddress.UserId = userAddressDto.UserId;
            existingUserAddress.Country = userAddressDto.Country;
            existingUserAddress.State = userAddressDto.State;
            existingUserAddress.City = userAddressDto.City;
            existingUserAddress.Zip = userAddressDto.Zip;
            existingUserAddress.Address = userAddressDto.Address;
            existingUserAddress.UpdatedAt = DateTime.UtcNow;

             _context.UserAddresses.Update(existingUserAddress);
              await _context.SaveChangesAsync();

            return Ok(existingUserAddress);
        }

    }
}
