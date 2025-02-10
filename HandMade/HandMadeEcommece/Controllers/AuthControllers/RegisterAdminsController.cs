using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HandMadeEcommece.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterAdminsController : ControllerBase
    {
        private readonly IAuth _auth;
        public RegisterAdminsController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAdminAsync([FromForm] RegisterAdminDto registerAdminDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var admin = await _auth.RegisterAdminAsync(registerAdminDto);
            if (!admin.IsAuthenticated)
                return BadRequest(admin.Message);
            return Ok(admin);
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInAdminAsync([FromForm] LogInAdmin logInAdmin)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var admin = await _auth.LogInAdminAsync(logInAdmin);
            if (!admin.IsAuthenticated)
                return BadRequest(admin.Message);
            return Ok(admin);
        }
    }
}
