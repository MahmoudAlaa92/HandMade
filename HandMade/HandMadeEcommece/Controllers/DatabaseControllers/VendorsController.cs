using HandMadeEcommece.Models;
using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
using Microsoft.EntityFrameworkCore;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly IAuth _auth;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public VendorsController(AppDbContext _Context, IMapper mapper, IAuth auth, IHttpContextAccessor contextAccessor, IWebHostEnvironment webHostEnvironment)
        {
            Context = _Context;
            _Mapper = mapper;
            _auth = auth;
            _contextAccessor = contextAccessor;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<IActionResult> GetVendor()
        {
            var vendors = await Context.Vendors.ToListAsync();
            if (vendors.Count <= 0) return NotFound();
            return Ok(vendors);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateVendor(int id, VendorDto vendorDto)
        {
            if (!ModelState.IsValid || id <= 0 || vendorDto == null) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(id);
            if (vendor == null) return NotFound();
            if (vendor.UserName != vendorDto.UserName && await Context.Vendors.FirstOrDefaultAsync(e=>e.UserName == vendorDto.UserName) != null) return BadRequest("The UserName Is Found");
           var httpContext = _contextAccessor.HttpContext;
            vendor.UserName = vendorDto.UserName;
            vendor.Phone = vendorDto.Phone;
            vendor.Image = vendorDto.Image;
            vendor.LName = vendorDto.LName;
            vendor.FName = vendorDto.FName;
            vendor.Description = vendorDto.Description;
            vendor.InstaLink = vendorDto.InstaLink;
            vendor.TwLink = vendorDto.TwLink;
            vendor.FbLink = vendorDto.FbLink;
            vendor.Status = vendorDto.Status;
            vendor.ShopName = vendorDto.ShopName;
            vendor.Banner = vendorDto.Banner;

            Context.Vendors.Update(vendor);
            await Context.SaveChangesAsync();
            return Ok(vendor);
        }

        [HttpPut("ChangeVendorPassword")]
        public async Task<IActionResult> ChangeVendorPassword([FromBody] ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(changePassword.Message);
            var result = await _auth.ChangePassword(changePassword);
            if (!result.IsChange)
                return BadRequest(changePassword.Message);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVendor(int id)
        {
            if (id <= 0) return BadRequest();
            var vendor = await Context.Vendors.FindAsync(id);
            if (vendor == null) return NotFound();
            Context.Vendors.Remove(vendor);
            await Context.SaveChangesAsync();
            return Ok(vendor);
        }
    }
}
