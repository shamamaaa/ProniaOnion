using Microsoft.AspNetCore.Mvc;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Users;

namespace ProniaOnion.API.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly IAuthenticationService _service;

        public UsersController(IAuthenticationService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerDto)
        {
            await _service.Register(registerDto);
            return NoContent();
        }

        [HttpPost("[Action]")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginDto)
        {
            return Ok(await _service.Login(loginDto));
        }

    }
}

