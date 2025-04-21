using Microsoft.AspNetCore.Mvc;
using MsConfigService.Services;

namespace MsConfigService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
        private readonly IGitConfigService _gitConfigService;

        public ConfigController(IGitConfigService gitConfigService)
        {
            _gitConfigService = gitConfigService;
        }

        [HttpGet("{applicationName}/{profile}")]
        public IActionResult GetConfiguration(string applicationName, string profile)
        {
            string configJson = _gitConfigService.GetConfiguration(applicationName, profile);

            if (configJson != null)
            {
                return Content(configJson, "application/json");
            }
            else
            {
                return NotFound($"Configuration not found for application '{applicationName}' and profile '{profile}'.");
            }
        }
    }
}