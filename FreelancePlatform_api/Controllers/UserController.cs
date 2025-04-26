using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;

namespace FreelancerPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _iuserService;

        public UsersController(IUserService iuserService)
        {
            _iuserService = iuserService;
        }

        // Реєстрація
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var result = await _userService.RegisterUserAsync(user);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Авторизація
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.AuthenticateUserAsync(model.Email, model.Password);
            return result.Success ? Ok(result.Message) : Unauthorized(result.Message);
        }

        // Оновлення профілю
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile([FromBody] User user)
        {
            var result = await _userService.UpdateUserProfileAsync(user);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        // Поповнення балансу
        [HttpPost("balance/deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deposit([FromQuery] int userId, [FromQuery] decimal amount)
        {
            var result = await _userService.AdjustBalanceAsync(userId, amount);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        // Отримання всіх фрілансерів
        [HttpGet("freelancers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreelancers()
        {
            var freelancers = await _userService.GetFreelancersAsync();
            return Ok(freelancers);
        }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

