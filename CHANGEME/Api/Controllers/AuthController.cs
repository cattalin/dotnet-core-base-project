using Infrastructure.Base;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Core.Models;

namespace Api.Controllers
{
    [Route("api/auth")]
    public class AuthController : BaseController
    {
        private UsersManager usersManager { get; set; }
        private AuthManager authManager { get; set; }

        public AuthController(
            UsersManager usersManager,
            AuthManager authManager,

            ILogger<AuthController> logger
        ) : base(logger)
        {
            this.usersManager = usersManager;
            this.authManager = authManager;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] AuthLoginDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = "Please complete all the fields" });

            var status = authManager.Login(payload);

            if(status.IsSuccessful)
                return Ok(status);
            else 
                return Unauthorized(status);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register([FromBody]AuthRegisterDto payload)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { Status = "Please complete all the fields" });

            var status = authManager.Register(payload);

            if (status.IsSuccessful)
                return Ok(status);
            else
                return BadRequest(status);
        }
    }
}

