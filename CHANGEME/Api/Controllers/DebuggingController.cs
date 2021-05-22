using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Api.Controllers
{
    [Route("api/debugging")]
    public class DebuggingController : BaseController
    {
        public DebuggingController()
        {
        }

        [HttpGet("env")]
        public IActionResult GetEnv()
        {
            var env1 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return Ok(new { env1 });
        }
    }
}
