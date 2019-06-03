using System;
using AspNetCoreTest.Models;
using Microsoft.AspNetCore.Mvc;
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
            var expire = DateTime.Now.AddDays(7);

            return Json(new { Token = _jwtService.CreateEncodedJwt(user, "12345", expire) });
        }
    }
}