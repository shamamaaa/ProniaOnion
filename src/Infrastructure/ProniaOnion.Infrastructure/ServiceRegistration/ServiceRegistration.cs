﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ProniaOnion.Application.Abstractions.Services;
using TokenHandler = ProniaOnion.Infrastructure.Implementations.TokenHandler;

namespace ProniaOnion.Infrastructure.ServiceRegistration
{
    public static class ServiceRegistration
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddAuthentication(opt =>
			{
				opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(opt =>
			{
				opt.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,

					ValidIssuer = configuration["Jwt:Issuer"],
					ValidAudience = configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecurityKey"])),
					LifetimeValidator = (notBefore, expired, token, param) => token != null ? expired > DateTime.UtcNow : false
                };
			});
			services.AddAuthorization();
			services.AddScoped<ITokenHandler, TokenHandler>();
			return services;
		}
	}
}

