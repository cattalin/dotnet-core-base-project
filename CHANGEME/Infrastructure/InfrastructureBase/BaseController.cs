using Infrastructure.Config;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Base
{
    [ApiController]
    public class BaseController : Controller
    {
        protected RuntimeSettings runtimeSettings { get; set; }
        protected ConnectionStringSettings connectionStrings { get; set; }

        public BaseController()
        {
        }

        public BaseController(IOptions<RuntimeSettings> runtimeSettings)
        {
            this.runtimeSettings = runtimeSettings.Value;
        }
    }
}
