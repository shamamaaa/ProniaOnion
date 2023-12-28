using System;
namespace ProniaOnion.Application.Dtos.Tokens
{
    public record TokenResponseDto(string Token, DateTime ExpireTime, string UserName);
}

