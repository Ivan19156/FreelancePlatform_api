using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DataAccess.Models;
using DataAccess.Services;
using FreelancePlatform.Core.DTOs.Users;
using System.Threading.Tasks;

namespace FreelancerPlatform.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // Реєстрація
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role
            };

            var result = await _userService.RegisterUserAsync(user);
            return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        // Авторизація
        // Авторизація
        [HttpPost("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            var result = await _userService.AuthenticateUserAsync(dto.Email, dto.Password);
            return result.Success ? Ok(result.Message) : Unauthorized(result.Message);
        }


        // Оновлення профілю
        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateUserDto dto)
        {
            var user = new User
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Role = dto.Role
            };
            var result = await _userService.UpdateUserProfileAsync(user);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        // Поповнення балансу
        [HttpPost("balance/deposit")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Deposit([FromBody] DepositBalanceDto dto)
        {
            var result = await _userService.AdjustBalanceAsync(dto.UserId, dto.Amount);
            return result.Success ? Ok(result.Message) : NotFound(result.Message);
        }

        // Отримання всіх фрілансерів
        [HttpGet("freelancers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFreelancers()
        {
            var freelancers = await _userService.GetFreelancersAsync();

            var freelancerDtos = freelancers.Select(f => new FreelancerDto
            {
                Id = f.Id,
                Name = f.Name,
                Rank = f.Rank,
                Email = f.Email
            });

            return Ok(freelancerDtos);
        }

    }


}


