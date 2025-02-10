using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminsController : ControllerBase
    {
        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuth _auth;
        public AdminsController(AppDbContext _Context, IMapper mapper, UserManager<AppUser> userManager, IAuth auth)
        {
            Context = _Context;
            _Mapper = mapper;
            _userManager = userManager;
            _auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdmin()
        {
            var admins = await Context.Admins.ToListAsync();
            if (admins.Count <= 0) return NotFound();
            return Ok(admins);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAdmin(int id, AdminDto adminDto)
        {
            if (!ModelState.IsValid || id <= 0 || adminDto == null) return BadRequest();
            var admin = await Context.Admins.FindAsync(id);
            if (admin == null) return NotFound();
            if (admin.UserName != adminDto.UserName && await _userManager.FindByNameAsync(adminDto.UserName) != null) return BadRequest("The UserName Is Found");
            admin.UserName = adminDto.UserName;
            admin.Phone = adminDto.Phone;
            admin.Image = await Methods.TransferImage(adminDto.image);
            admin.LName = adminDto.LName;
            admin.FName = adminDto.FName;
            admin.Salary = admin.Salary;
            Context.Admins.Update(admin);
            await Context.SaveChangesAsync();
            return Ok(admin);
        }

        [HttpPut("ChangeAdminPassword")]
        public async Task<IActionResult> ChangeAdminPassword([FromBody]ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(changePassword.Message);
            var result = await _auth.ChangePassword(changePassword);
            if (!result.IsChange)
                return BadRequest(changePassword.Message);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            if (id <= 0) return BadRequest();
            var admin = await Context.Admins.FindAsync(id);
            if (admin == null) return NotFound();
            Context.Admins.Remove(admin);
            await Context.SaveChangesAsync();
            return Ok(admin);
        }

    }
}
