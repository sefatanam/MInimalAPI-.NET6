interface ICustomerRepository
{
    void Create(Customer customer);
    void Delete(int id);
    List<Customer> GetAll();
    Customer GetById(int id);
    void Update(Customer customer);
}




