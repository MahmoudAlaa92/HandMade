using HandMadeEcommece.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandMadeEcommece.Controllers.AuthControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterUserController : ControllerBase
    {
        private readonly IAuth _auth;
        public RegisterUserController(IAuth auth)
        {
            _auth = auth;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterUser([FromForm] RegisterUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.RegisterUserAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }


        [HttpPost("LogIn")]
        public async Task<IActionResult> LogInUser(LogInUserModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _auth.LogInUserAsync(model);
            if (!result.IsAuthenticated)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
