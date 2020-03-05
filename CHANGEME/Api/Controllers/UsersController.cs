using Core.Config;
using Core.Infrastructure;
using Core.Managers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Api.Controllers
{
    [Route("api/users")]
    public class UsersController : BaseController
    {
        private UsersManager usersManager { get; set; }
        public UsersController(
            UsersManager usersManager,

            ILogger<UsersController> logger,
            IOptions<RuntimeSettings> appSettingsAccessor
        ) : base(logger, appSettingsAccessor)
        {
            this.usersManager = usersManager;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(settings.ConnectionString);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var xxx = DatabaseConnector.GetInstance().ConnectionString;
            var result = usersManager.GetById(id);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody]string value)
        {
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
