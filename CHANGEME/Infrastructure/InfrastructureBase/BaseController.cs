using Infrastructure.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Base
{
    [ApiController]
    public class BaseController : Controller
    {
        protected ILogger logger { get; set; }
        protected RuntimeSettings runtimeSettings { get; set; }
        protected ConnectionStringSettings connectionStrings { get; set; }

        public BaseController(
            ILogger logger
        ) {
            this.logger = logger;
        }

        public BaseController(
            IOptions<RuntimeSettings> runtimeSettings,
            ILogger logger
        ) {
            this.logger = logger;
            this.runtimeSettings = runtimeSettings.Value;
        }
    }
}
