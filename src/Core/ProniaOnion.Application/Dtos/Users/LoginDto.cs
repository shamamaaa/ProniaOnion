using System;
namespace ProniaOnion.Application.Dtos.Users
{
    public record LoginDto(string UserNameOrEmail, string Password);

}

