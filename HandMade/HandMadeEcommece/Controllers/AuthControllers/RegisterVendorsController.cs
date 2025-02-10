using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterVendorsController : ControllerBase
    {
        private readonly IAuth _auth;
        public RegisterVendorsController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterVendorAsync([FromForm] RegisterVendor registerVendor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vedor = await _auth.RegisterVendorAsync(registerVendor);
            if (!vedor.IsAuthenticated)
                return BadRequest(vedor.Message);
            return Ok(vedor);
        }
        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInVendorAsync([FromForm] LogInVendor logInVendor)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var vendor = await _auth.LogInVendorAsync(logInVendor);
            if (!vendor.IsAuthenticated)
                return BadRequest(vendor.Message);
            return Ok(vendor);
        }
    }
}
