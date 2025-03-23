using Moq;
using timelogger_web_api.Models.DTOs.CustomerDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.CustomerRepository;
using timelogger_web_api.Services.CustomerService;

namespace timelogger_tests.CustomerTests
{
    public class CustomerServiceTests
    {
        private readonly ICustomerService customerService;
        private readonly Mock<ICustomerRepository> customerRepositoryMock;

        public CustomerServiceTests()
        {
            customerRepositoryMock = new Mock<ICustomerRepository>();
            customerService = new CustomerService(customerRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_ReturnsCustomer()
        {
            // Arrange
            var request = new CreateCustomerDTORequest { Name = "Valid Customer" };
            var createdCustomer = new Customer { Id = Guid.NewGuid(), Name = request.Name };

            customerRepositoryMock
                .Setup(repo => repo.CreateCustomer(It.IsAny<Customer>()))
                .ReturnsAsync(createdCustomer);

            // Act
            var result = await customerService.CreateCustomer(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.Name, result.Name);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
