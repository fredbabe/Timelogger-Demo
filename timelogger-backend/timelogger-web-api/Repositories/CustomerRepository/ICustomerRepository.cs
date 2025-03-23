using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Repositories.CustomerRepository
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomer(Customer customer);

        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
