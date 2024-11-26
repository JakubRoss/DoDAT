using DoDAT.Presentation.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DoDAT.Presentation.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public AccountsController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public async Task Register([FromBody] UserDto user)
        {

            await _userRepository.Register(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserDto userDto)
        {
            var user = await _userRepository.LogIn(userDto);

            var claims = new List<Claim>()
            {
                new("NameIdentifier", user.Id.ToString()),
                new("Name", user.Login)
            };

            //Nie robilem wczesniej z cookie authetication tylko jwt gdzie uzywalem kluczy (sha256) o ktorych byla mowa 
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Ustawienie ciasteczka sesji
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return Ok(new { message = "Logged in successfully" });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(new { message = "Logged out successfully" });
        }

        [HttpGet("isLoggedIn")]
        public IActionResult IsLoggedIn()
        {
            return Ok(new { isLoggedIn = User.Identity!.IsAuthenticated });
        }
    }
    // DTOs
    public class UserDto
    {
        [Required(ErrorMessage = "Login is required.")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Login cannot be longer than 100 characters.")]
        public string Login { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; }
    }

}
