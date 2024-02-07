using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

// configure OTel and OTLP
const string serviceName = "yarpProxy";
// AppContext.SetSwitch("Azure.Experimental.EnableActivitySource", true);

builder.Logging.AddOpenTelemetry(options =>
{
    options
        .SetResourceBuilder(
            ResourceBuilder.CreateDefault()
                .AddService(serviceName))
        .AddOtlpExporter();
});

builder.Services.AddOpenTelemetry()
    .ConfigureResource(resource => resource.AddService(serviceName))
    .WithTracing(tracing => tracing
        .AddAspNetCoreInstrumentation()
        // enable YARP distributed tracing
        // see https://microsoft.github.io/reverse-proxy/articles/distributed-tracing.html
        .AddSource("Yarp.ReverseProxy") 
        .AddOtlpExporter()
    )
    .WithMetrics(metrics => metrics
        .AddAspNetCoreInstrumentation()
        .AddOtlpExporter());

// build and start app
var app = builder.Build();
app.MapReverseProxy();
app.Run();