using Calabonga.AspNetCore.AppDefinitions;
using Scalar.AspNetCore;

namespace Calabonga.AppDefinitions.Web.Definitions.Commons;

/// <summary>
/// Common definition
/// </summary>
public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();
    }

    public override void ConfigureApplication(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference();
        }

        app.UseHttpsRedirection();
    }
}
