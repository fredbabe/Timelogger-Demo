using timelogger_web_api.Models.DTOs.CustomerDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Services.CustomerService
{
    public interface ICustomerService
    {
        public Task<Customer> CreateCustomer(CreateCustomerDTORequest request);

        public Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
