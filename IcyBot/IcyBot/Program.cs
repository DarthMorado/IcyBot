using IcyBot;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Runtime;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, config) =>
        {
            var env = context.HostingEnvironment;

            config//.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                  .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
        })
        .ConfigureServices((context, services) =>
        {
            services.AddHostedService<App>();
            services.Configure<AppSettings>(context.Configuration.GetSection("AppSettings"));
            Configure(services, context.Configuration);
        });



#if DEBUG
        // Run as a console app in Debug mode
        builder.UseConsoleLifetime();
#else
// Run as a Windows Service in Release mode
builder.UseWindowsService();
#endif

        var host = builder.Build();
        host.Run();
    }

    public static void Configure(IServiceCollection services, IConfiguration config)
    {
    }

}