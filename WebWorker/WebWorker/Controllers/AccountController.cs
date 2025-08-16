using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using WebWorker.Data.Entities.Identity;
using WebWorker.Interfaces;
using WebWorker.Models.Account;

namespace WebWorker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(UserManager<UserEntity> userManager,
        IJwtTokenService jwtTokenService) : ControllerBase
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized("Invalid email or password.");
            }
            var result = await userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return Unauthorized("Invalid email or password.");
            }
            //
            var token = await jwtTokenService.GenerateTokenAsync(user);

            return Ok(
                new
                {
                    token 
                });
        }
    
    
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest("Email and password are required.");
            }
            var user = await userManager.FindByEmailAsync(model.Email);
            if(user == null)
            {
                return BadRequest("User with this email already exists.");
            }
            user = new UserEntity
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            if (model.ImageFile != null)
            {
                var imageName = $"{Guid.NewGuid()}{Path.GetExtension(model.ImageFile.FileName)}";
                var path = Path.Combine(Directory.GetCurrentDirectory(), "images", imageName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await model.ImageFile.CopyToAsync(stream);
                }
                user.Image = imageName;
            }
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(e => e.Description));
            }
            // Optionally, assign a default role to the user
            await userManager.AddToRoleAsync(user, Constants.Roles.User);
            var token = await jwtTokenService.GenerateTokenAsync(user);
            return Ok(new
            {
                token
            });
        }
    }
}
