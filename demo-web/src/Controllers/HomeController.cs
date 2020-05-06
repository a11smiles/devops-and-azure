using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using demo_web.Models;
using Microsoft.ApplicationInsights;

namespace demo_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly TelemetryClient _telemetry;

        public HomeController(ILogger<HomeController> logger, TelemetryClient telemetry)
        {
            _logger = logger;
            _telemetry = telemetry;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CalcForm(Models.CalcModel calcs)
        {
            try {
                int num1, num2;

                if (!Int32.TryParse(calcs.Num1, out num1) || !Int32.TryParse(calcs.Num2, out num2))
                    throw new ArgumentException("Value must be a number.");
                else if (num1 > 100 || num1 < 0 || num2 > 100 || num2 < 0)
                    throw new ArithmeticException("Value out of range.");
                else if (num2 == 0)
                    throw new DivideByZeroException("Denominator cannot be 0.");
                else 
                    calcs.Result = num1 / num2;

                return View(
                    "Index",
                    calcs
                );

            }
            catch (Exception e) {
                e.Data.Add("CalcsData", calcs);
                _telemetry.TrackException(e, null, null);
                throw;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
