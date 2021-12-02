using NBomber.CSharp;
using NBomber.Contracts;
using NBomber.Plugins.Http.CSharp;

var httpFactory = HttpClientFactory.Create();

var minimalApiStep = Step.Create("minimal_api_setup", httpFactory, async context =>
 {
     var response = await context.Client.GetAsync("https://localhost:7136/customers/1");
     return response.IsSuccessStatusCode ? Response.Ok(statusCode: (int)response.StatusCode) : Response.Fail(statusCode: (int)response.StatusCode);
 });


var minimalApiSenario = ScenarioBuilder.CreateScenario("minimal_api", minimalApiStep)
    .WithWarmUpDuration(TimeSpan.FromSeconds(5))
    .WithLoadSimulations(Simulation.KeepConstant(24, TimeSpan.FromSeconds(60)));


NBomberRunner.RegisterScenarios(minimalApiSenario).Run();