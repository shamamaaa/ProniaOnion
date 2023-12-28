using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Tokens;
using ProniaOnion.Application.Dtos.Users;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly ITokenHandler _handler;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationService(IMapper mapper, ITokenHandler handler,UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _handler = handler;
            _userManager = userManager;
        }

        public async Task Register(RegisterDto registerDto)
        {
            AppUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == registerDto.UserName || u.Email == registerDto.Email);
                if (user is not null) throw new Exception("User already exist");
            user = _mapper.Map<AppUser>(registerDto);

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                StringBuilder message = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    message.AppendLine(error.Description);
                }
                throw new Exception(message.ToString());
            }
        }

        public async Task<TokenResponseDto> Login(LoginDto loginDto)
        {
            AppUser user = await _userManager.FindByNameAsync(loginDto.UserNameOrEmail);
            if (user is null)
            {
                user = await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail);
                if (user is null) throw new Exception("User not found, please input true username/email or password");
            }
            if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                throw new Exception("User not found, please input true username/email or password");
            }

            return _handler.CreateJwt(user, 60);
        }
    }
}

