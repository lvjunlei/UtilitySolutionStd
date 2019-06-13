using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AspNetCoreTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Utility.Security;

namespace AspNetCoreTest.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        public IActionResult Get(string account)
        {
            var expire = DateTime.Now.AddDays(7);
            var user = new User { Account = account };
            return Json(new { Token = _jwtService.CreateEncodedJwt(user, "12345", expire) });
        }

        [HttpGet]
        public IActionResult GetUser()
        {
            return Json(new User { Account = "Test" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login([FromBody]User user)
        {
            if (!string.IsNullOrEmpty(user.Account) && !string.IsNullOrEmpty(user.Password))
            {
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
                    new Claim(ClaimTypes.Name, user.Account),
                    new Claim("Account", user.Account)
                };
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Const.SecurityKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: Const.Domain,
                    audience: Const.Domain,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials: creds);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token)
                });
            }
            else
            {
                return BadRequest(new { message = "username or password is incorrect." });
            }
            //var expire = DateTime.Now.AddDays(7);

            //return Json(new { Token = _jwtService.CreateEncodedJwt(user, "12345", expire) });
        }
    }
}