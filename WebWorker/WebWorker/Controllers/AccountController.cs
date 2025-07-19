using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
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

            // Validate the Google token here (e.g., using Google API)
            // For demonstration purposes, we'll assume the token is valid and retrieve user info.
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", model.Token);
            var response = await httpClient.GetAsync($"https://www.googleapis.com/oauth2/v2/userinfo");
            if (!response.IsSuccessStatusCode)
            {
                return Unauthorized("Invalid Google token.");
            }
            var userJson = await response.Content.ReadAsStringAsync();

            return Ok(userJson);
        }
    }
}
