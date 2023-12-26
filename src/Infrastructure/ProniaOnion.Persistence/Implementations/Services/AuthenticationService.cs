using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion.Application.Abstractions.Services;
using ProniaOnion.Application.Dtos.Users;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Persistence.Implementations.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;

        public AuthenticationService(IMapper mapper, IConfiguration configuration, UserManager<AppUser> userManager)
        {
            _mapper = mapper;
            _configuration = configuration;
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

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

            SigningCredentials signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha512Signature);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
               issuer: _configuration["Jwt:Issuer"],
               audience: _configuration["Jwt:Audience"],
               claims:claims,
               notBefore:DateTime.UtcNow,
               expires:DateTime.UtcNow.AddHours(1),
               signingCredentials:signingCredentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler(); //jwt tokeni stringe cevirmek ucun
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return new(token, jwtSecurityToken.ValidTo, user.UserName);
        }
    }
}

