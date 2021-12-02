namespace MinimalAPI.Sauce;
public interface IEndpoints
{
    void DefineEndpoints(WebApplication app);
    void DefineServices(IServiceCollection services);
}