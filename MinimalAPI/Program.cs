
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<CustomerRepository>();
var app = builder.Build();


app.MapGet("/customers", ([FromServices] CustomerRepository repo) =>
{
    return repo.GetAll();
});


app.MapGet("/customers/{id}", ([FromServices] CustomerRepository repo, int id) =>
{
    var customer = repo.GetById(id);
    return customer is not null ? Results.Ok(customer) : Results.NoContent();
});


app.MapPost("/customers", ([FromServices] CustomerRepository repo, Customer customer) =>
{
    repo.Create(customer);
    return Results.Created($"/customers/{customer.Id}", customer);
});



app.MapPut("/customers/{id}", ([FromServices] CustomerRepository repo, int id, Customer customer) =>
{
    var existingCustomer = repo.GetById(id);
    if (existingCustomer is null) return Results.NotFound();

    repo.Update(customer);

    return Results.Ok(customer);

});


app.MapDelete("/customers/{id}", ([FromServices] CustomerRepository repo, int id) =>
{
    var existingCustomer = repo.GetById(id);

    if (existingCustomer is null) return Results.NotFound();

    repo.Delete(id);

    return Results.Ok();

});

app.Run();






record Customer(int Id, string Name);


class CustomerRepository
{
    private readonly Dictionary<int, Customer> _customers = new Dictionary<int, Customer>();

    public void Create(Customer customer)
    {
        if (customer is null) return;
        _customers[customer.Id] = customer;
    }

    public Customer GetById(int id) => _customers[id];

    public void Delete(int id) => _customers.Remove(id);

    public void Update(Customer customer)
    {
        var existingCustomer = GetById(customer.Id);
        if (existingCustomer is null) return;
        _customers[customer.Id] = customer;
    }

    public List<Customer> GetAll() => _customers.Values.ToList();
}




