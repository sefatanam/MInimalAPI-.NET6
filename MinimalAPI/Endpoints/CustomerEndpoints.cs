using MinimalAPI.Sauce;
namespace MinimalAPI.Endpoints;

public class CustomerEndpoints : IEndpoints
{
    public void DefineEndpoints(WebApplication app)
    {

        app.MapGet("/customers", GetCustomers);
        app.MapGet("/customers/{id}", GetCustomerById);
        app.MapPost("/customers", CreateCustomer);
        app.MapPut("/customers/{id}", UpdateCustomer);
        app.MapDelete("/customers/{id}", DeleteCustomer);

    }

    internal IResult GetCustomerById(ICustomerRepository repo, int id)
    {
        var customer = repo.GetById(id);
        return customer is not null ? Results.Ok(customer) : Results.NoContent();
    }

    internal List<Customer> GetCustomers(ICustomerRepository repo)
    {
        return repo.GetAll();
    }

    internal IResult CreateCustomer(ICustomerRepository repo, Customer customer)
    {
        repo.Create(customer);
        return Results.Created($"/customers/{customer.Id}", customer);

    }

    internal IResult UpdateCustomer(ICustomerRepository repo, int id, Customer customer)
    {
        var existingCustomer = repo.GetById(id);
        if (existingCustomer is null) return Results.NotFound();
        repo.Update(customer);
        return Results.Ok(customer);
    }

    internal IResult DeleteCustomer(ICustomerRepository repo, int id)
    {
        var existingCustomer = repo.GetById(id);
        if (existingCustomer is null) return Results.NotFound();
        repo.Delete(id);
        return Results.Ok();
    }

    public void DefineServices(IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, CustomerRepository>();
    }
}