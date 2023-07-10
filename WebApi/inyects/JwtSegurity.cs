using System;
using System.Text;
using Entities.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.inyects
{
	public static class JwtSegurity
	{
		public static void AddJwt(this IServiceCollection service, IConfiguration configuration)
		{
			service.Configure<JwtSetting>(
				configuration.GetSection("JwtSetting")
			);
			service.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(o =>
			{
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidIssuer = configuration["JwtSetting:Issuer"],
					ValidAudience = configuration["JwtSetting:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"])),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = false,
					ValidateIssuerSigningKey = true
				};
			});

			service.AddAuthorization();
		}
	}
}

