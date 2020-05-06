using System.Collections.Generic;
using Microsoft.ApplicationInsights;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

public class CustomExceptionFilter : ExceptionFilterAttribute {

    private readonly TelemetryClient _telemetry;

    public CustomExceptionFilter(TelemetryClient telemetry) {
        _telemetry = telemetry;
    }

    public override void OnException(ExceptionContext context) {

        context.ExceptionHandled = true;

        Dictionary<string, string> props = new Dictionary<string, string>();
        foreach(string key in context.Exception.Data.Keys)
            props.Add(key, JsonConvert.SerializeObject(context.Exception.Data[key]));

        _telemetry.TrackException(context.Exception, props, null);
    }
}
