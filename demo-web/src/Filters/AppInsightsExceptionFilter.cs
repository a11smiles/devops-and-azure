using System;
using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
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
            // Create Correlation Id
            string corrId = Guid.NewGuid().ToString();

            // Inform app engine that exception is handled so App Insights doesn't log exception twice
            context.ExceptionHandled = true;

            // Add Correlation Id and custom data to App Insights 'customDimensions'
            Dictionary<string, string> props = new Dictionary<string, string>();
            props.Add("Correlation Id", corrId);

            foreach (string key in context.Exception.Data.Keys)
                props.Add(key, JsonConvert.SerializeObject(context.Exception.Data[key]));

            // Log exception with App Insights
            _telemetry.TrackException(context.Exception, props, null);

            // Redirect to error page
            context.Result = new RedirectToRouteResult(new { area = "", controller = "home", action = "error", id = corrId });
        }
    }
}