using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GoSmartValue.Web.Controllers
{
    [Route("auth/")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("accessdenied")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpPost]
        [Route("token")]
        public ActionResult Token()
        {
            //Security Key
            var securityKey = _configuration["AuthKey"];
            //Symmetric security Key
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            //Signing Credentials
            var signInCredentials = new SigningCredentials(symmetricKey,SecurityAlgorithms.HmacSha256);
            //Create token
            var token = new JwtSecurityToken(
                issuer:"gosmartvalue.com",
                audience:"readers",
                expires: DateTime.Now.AddHours(3),
                signingCredentials: signInCredentials
            );
            //return the token
            return Ok(new JwtSecurityTokenHandler().WriteToken(token));
        }
    }
}