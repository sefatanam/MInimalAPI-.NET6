
using MinimalAPI.Sauce;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpoints(typeof(Customer));
var app = builder.Build();


app.UseEndpoints();
app.Run();
