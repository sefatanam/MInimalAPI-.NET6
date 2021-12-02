
class CustomerRepository : ICustomerRepository
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




