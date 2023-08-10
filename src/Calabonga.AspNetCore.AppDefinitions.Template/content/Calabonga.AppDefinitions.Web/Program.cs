using Calabonga.AspNetCore.AppDefinitions;
using Serilog;

try
{
    // created builder
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog((context, configuration) =>
        configuration.ReadFrom.Configuration(context.Configuration));

    // adding definitions for application
    builder.AddDefinitions(typeof(Program));

    // create application
    var app = builder.Build();

    // using definition for application
    app.UseDefinitions();

    // using Serilog request logging
    app.UseSerilogRequestLogging();

    // start application
    app.Run();

    return 0;
}
catch (Exception ex)
{
    var type = ex.GetType().Name;
    if (type.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}