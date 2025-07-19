using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebWorker.Data.Entities.Identity;
using WebWorker.Models.Account;

namespace WebWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<UserEntity> userManager) : ControllerBase
    {
        [HttpPost("google-login")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Token))
            {
                return BadRequest("Token is required.");
            }
            return Ok();
        }
    }
}
