using Core.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Core.Infrastructure
{
    public class BaseController : Controller
    {
        protected ILogger logger { get; set; }
        protected RuntimeSettings settings { get; set; }

        public BaseController(
            ILogger logger,
            IOptions<RuntimeSettings> appSettingsAccessor)
        {
            this.logger = logger;
            this.settings = appSettingsAccessor.Value;
        }
    }
}
