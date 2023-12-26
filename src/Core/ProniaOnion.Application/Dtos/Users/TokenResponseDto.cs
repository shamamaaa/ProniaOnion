using System;
namespace ProniaOnion.Application.Dtos.Users
{
	public record TokenResponseDto(string Token, DateTime Expires, string UserName);

}

