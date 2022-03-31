using JWTDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace JWTDemo.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[Controller]/[Action]")]
    public class NameController : Controller
    {
        private readonly IJwtAuthenticationManager jwtAuthenticationManager;
        public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.jwtAuthenticationManager = jwtAuthenticationManager;
        }
       

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "hello", "ji", "Hi", "mint" };
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
    public IActionResult Authenticate([FromBody] UserCread usercread)
        {
          var token =  jwtAuthenticationManager.Authenticate(usercread.userName, usercread.password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }

       
    }
}
