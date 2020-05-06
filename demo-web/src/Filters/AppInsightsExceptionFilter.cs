using System.Collections.Generic;
using System.Diagnostics;
using demo_web.Models;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;

namespace demo_web.Filters
{
    public class AppInsightsExceptionFilter : ExceptionFilterAttribute
    {

        private readonly TelemetryClient _telemetry;

        public AppInsightsExceptionFilter(TelemetryClient telemetry)
        {
            _telemetry = telemetry;
        }

        public override void OnException(ExceptionContext context)
        {

            context.ExceptionHandled = true;

            Dictionary<string, string> props = new Dictionary<string, string>();
            foreach (string key in context.Exception.Data.Keys)
                props.Add(key, JsonConvert.SerializeObject(context.Exception.Data[key]));

            _telemetry.TrackException(context.Exception, props, null);

            context.Result = new RedirectToRouteResult(new { area = "", controller = "home", action = "error" });
        }
    }
}