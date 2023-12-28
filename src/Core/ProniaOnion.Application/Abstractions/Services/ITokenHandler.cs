using System;
using ProniaOnion.Application.Dtos.Tokens;
using ProniaOnion.Domain.Entities;

namespace ProniaOnion.Application.Abstractions.Services
{
	public interface ITokenHandler
	{
		TokenResponseDto CreateJwt(AppUser user, int minutes);
	}
}

