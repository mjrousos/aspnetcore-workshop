using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreWorkshop.Models;
using Microsoft.Extensions.Options;

namespace AspNetCoreWorkshop.Controllers
{
    [Route("/api/[controller]")]
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
                Settings = _settings.Settings
                            .Where(s => s.Value.Enabled)
            });
        }
    }
}