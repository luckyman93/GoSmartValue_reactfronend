using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AV.Common.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace GoSmartValue.Web.AppStartConfigs
{
    public static class AuthConfigurations
    {
        private static readonly string KeyValue = Environment.GetEnvironmentVariable("AuthKey");
        /// <summary>
        /// Generate JWT Bearer Token
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public static string GetJwtToken(User user,IList<string> roles) {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyValue));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //create claims based on user details passed
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                new Claim(JwtRegisteredClaimNames.Nbf, DateTimeOffset.Now.ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, DateTimeOffset.Now.AddHours(12).ToUnixTimeSeconds().ToString())
            };
            foreach (var role in roles)
            {
                var claim = new Claim(ClaimTypes.Role, role);
                claims.Add(claim);
            }
            var token = new JwtSecurityToken(
                "gosmartvalue.com", "gosmartvalue.com", claims, DateTime.Now,
               expires: DateTime.Now.AddDays(7),
               signingCredentials: credentials
           );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(opts =>
                {
                    //opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    //opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    // Cookie settings
                    options.ExpireTimeSpan = TimeSpan.FromDays(3);
                    options.LoginPath = new PathString($"/Account/Login");
                    options.LogoutPath = new PathString($"/Account/Logout");
                    options.AccessDeniedPath = new PathString($"/auth/AccessDenied");
                    options.SlidingExpiration = true;
                })
                .AddJwtBearer(x =>
                {
                    
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = tokenValidationParameters();
                });
        }

        private static TokenValidationParameters tokenValidationParameters()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KeyValue));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenValidationParams = new TokenValidationParameters()
            {

                ValidateAudience = true,
                ValidateLifetime = true,
                RequireSignedTokens = true,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = "gosmartvalue.com",
                ValidIssuer = "gosmartvalue.com",
                IssuerSigningKey = credentials.Key,
                ClockSkew = TimeSpan.FromMinutes(3)
            };
            return tokenValidationParams;
        }
    }
}
