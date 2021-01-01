using Infrastructure.Config;
using Infrastructure.Base;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;

namespace Api.Controllers
{
    [Route("api/debugging")]
    public class DebuggingController : BaseController
    {
        public DebuggingController(IOptions<RuntimeSettings> runtimeSettings)
        {
        }

        [HttpGet("env")]
        public IActionResult GetAzureEnv()
        {
            var env1 = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            return Ok(new { env1 });
        }
    }
}
