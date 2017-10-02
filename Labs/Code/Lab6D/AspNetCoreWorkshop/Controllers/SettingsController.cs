using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using AspNetCoreWorkshop.Models;

namespace AspNetCoreWorkshop.Controllers
{
    [Produces("application/json")]
    [Route("api/Settings")]
    public class SettingsController : Controller
    {
        private StoreSettingsOptions _settings;

        public SettingsController(IOptions<StoreSettingsOptions> settingsOptions)
        {
            _settings = settingsOptions?.Value;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(new
            {
                Name = _settings.StoreName,
                ID = _settings.StoreID,
                Settings = _settings.Settings.Where(s => s.Value.Enabled)
            });
        }
    }
}