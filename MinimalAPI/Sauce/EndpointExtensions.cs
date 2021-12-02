namespace MinimalAPI.Sauce;
public static class EndpointExtensions
{
    public static void AddEndpoints(this IServiceCollection services, params Type[] scanMarkers)
    {
        var endpoints = new List<IEndpoints>();

        foreach (var scanMarker in scanMarkers)
        {
            endpoints.AddRange(scanMarker.Assembly.ExportedTypes.Where(x =>
            typeof(IEndpoints).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract
            ).Select(Activator.CreateInstance).Cast<IEndpoints>().ToList());
        }

        foreach (var endpoint in endpoints)
        {
            endpoint.DefineServices(services);
        }

        services.AddSingleton(endpoints as IReadOnlyCollection<IEndpoints>);
    }

    public static void UseEndpoints(this WebApplication app)
    {
        var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpoints>>();

        foreach (var definition in definitions)
        {
            definition.DefineEndpoints(app);
        }
    }
}