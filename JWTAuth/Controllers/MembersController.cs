using JWTAuth.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWTAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IJwtAuth jwtAuth;

        private readonly List<Member> members = new List<Member>()
        {
            new Member{Id=1, Name="Hassan" },
            new Member {Id=2, Name="Hassaan" },
            new Member{Id=3, Name="Ahmad"}
        };
        public MembersController(IJwtAuth jwtAuth)
        {
            this.jwtAuth = jwtAuth;
        }

        // GET: api/<MembersController>
        [HttpGet]
        public IEnumerable<Member> Get()
        {
            return members;
        }

        // GET api/<MembersController>/5
        [HttpGet("{id}")]
        public Member Get(int id)
        {
            return members.Find(m => m.Id == id);
        }

        [AllowAnonymous]
        // POST api/<MembersController>
        [HttpPost("authentication")]
        public IActionResult Authentication([FromBody] UserCredential userCredential)
        {
            var token = jwtAuth.Authentication(userCredential.UserName, userCredential.Password);
            if (token == null)
                return Unauthorized();
            return Ok(token);
        }
    }
}
