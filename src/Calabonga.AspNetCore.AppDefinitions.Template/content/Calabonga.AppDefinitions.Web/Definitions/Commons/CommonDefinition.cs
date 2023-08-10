using Calabonga.AspNetCore.AppDefinitions;

namespace Calabonga.AppDefinitions.Web.Definitions.Commons;

/// <summary>
/// Common definition
/// </summary>
public class CommonDefinition : AppDefinition
{
    public override void ConfigureServices(WebApplicationBuilder builder)
    {
        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }

    public override void ConfigureApplication(WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseStaticFiles();

        app.UseHttpsRedirection();
    }
}