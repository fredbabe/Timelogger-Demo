using timelogger_web_api.Models.DTOs.CustomerDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.CustomerRepository;

namespace timelogger_web_api.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<Customer> CreateCustomer(CreateCustomerDTORequest request)
        {
            var currentDate = DateTime.Now;

            var customer = new Customer
            {
                Name = request.Name,
                CreatedOn = currentDate,
                UpdatedOn = currentDate
            };

            return await customerRepository.CreateCustomer(customer);
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            return await customerRepository.GetAllCustomers();
        }
    }
}
