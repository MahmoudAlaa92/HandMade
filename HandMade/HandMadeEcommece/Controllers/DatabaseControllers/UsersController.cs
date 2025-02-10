using HandMadeEcommece.Models;
using HandMadeEcommece.Models.Data;
using HandMadeEcommece.Models.Dto;
using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Numerics;

namespace HandMadeEcommece.Controllers.DatabaseControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private readonly IMapper _Mapper;
        private readonly AppDbContext Context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IAuth _auth;

        public UsersController(AppDbContext _Context, IMapper mapper, UserManager<AppUser> userManager, IAuth auth)
        {
            Context = _Context;
            _Mapper = mapper;
            this._userManager = userManager;
            this._auth = auth;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var users = await _userManager.GetUsersInRoleAsync("Customer");
            if (users.Count <= 0) return NotFound();
            return Ok(users);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(int id, UserDto userDto)
        {
            if (!ModelState.IsValid || id <= 0 || userDto == null) return BadRequest();
            var user = await Context.Users.FindAsync(id);
            if (user == null) return NotFound();
            if (user.UserName != userDto.UserName && await _userManager.FindByNameAsync(userDto.UserName) != null) return BadRequest("The UserName Is Found");
            user.UserName = userDto.UserName;
            user.Phone = userDto.Phone;
            user.Image = await Methods.TransferImage(userDto.image);
            user.LName = userDto.LName;
            user.FName = userDto.FName;
            Context.Users.Update(user);
            await Context.SaveChangesAsync();
            return Ok(user);
        }

        [HttpPut("ChangeUserPassword")]
        public async Task<IActionResult> ChangeUserPassword([FromBody] ChangePasswordDto changePassword)
        {
            if (!ModelState.IsValid)
                return BadRequest(changePassword.Message);
            var result = await _auth.ChangePassword(changePassword);
            if (!result.IsChange)
                return BadRequest(changePassword.Message);
            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (id <= 0) return BadRequest();
            var user = await Context.Users.FindAsync(id);
            if (user == null) return NotFound();
            Context.Users.Remove(user);
            await Context.SaveChangesAsync();
            return Ok(user);
        }

    }
}

