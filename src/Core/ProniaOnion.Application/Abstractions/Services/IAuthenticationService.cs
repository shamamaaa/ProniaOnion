using System;
using ProniaOnion.Application.Dtos.Tokens;
using ProniaOnion.Application.Dtos.Users;

namespace ProniaOnion.Application.Abstractions.Services
{
	public interface IAuthenticationService
	{
		Task Register(RegisterDto registerDto);
        Task<TokenResponseDto> Login(LoginDto loginDto);
    }
}

