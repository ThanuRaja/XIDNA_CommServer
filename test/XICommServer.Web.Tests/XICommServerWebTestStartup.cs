using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace XICommServer;

public class XICommServerWebTestStartup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication<XICommServerWebTestModule>();
    }

    public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
    {
        app.InitializeApplication();
    }
}
