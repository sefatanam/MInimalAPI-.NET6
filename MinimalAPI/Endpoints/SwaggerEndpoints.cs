using MinimalAPI.Sauce;

namespace MinimalAPI.Endpoints;

public class SwaggerEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MinimalAPI"));
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
        { Title = "Minimal API", Version = "v1" }));
    }
}