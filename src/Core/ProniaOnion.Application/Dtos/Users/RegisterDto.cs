using System;
namespace ProniaOnion.Application.Dtos.Users
{
	public record RegisterDto(string UserName, string Email, string Password, string ConfirmPassword, string Name, string Surname);
}

