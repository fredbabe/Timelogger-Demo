using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using timelogger_web_api.Controllers;
using timelogger_web_api.Models.DTOs.CustomerDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.CustomerService;

namespace timelogger_tests.CustomerTests
{
    public class CustomerControllerTests
    {
        private readonly CustomerController customerController;
        private readonly Mock<ICustomerService> customerServiceMock;
        private readonly Mock<ILogger<CustomerController>> loggerMock;

        public CustomerControllerTests()
        {
            customerServiceMock = new Mock<ICustomerService>();
            loggerMock = new Mock<ILogger<CustomerController>>();
            customerController = new CustomerController(customerServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task CreateCustomer_ValidRequest_ReturnsCreatedCustomer()
        {
            // Arrange
            var customerRequest = new CreateCustomerDTORequest { Name = "New Customer" };
            var createdCustomer = new Customer { Id = Guid.NewGuid(), Name = "New Customer" };

            customerServiceMock
                .Setup(x => x.CreateCustomer(customerRequest))
                .ReturnsAsync(new Customer { Id = Guid.NewGuid(), Name = customerRequest.Name });

            // Act
            var result = await customerController.CreateCustomer(customerRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedCustomer = Assert.IsType<Customer>(okResult.Value);

            Assert.NotEqual(Guid.Empty, returnedCustomer.Id);
            Assert.Equal(customerRequest.Name, returnedCustomer.Name);
        }
    }
}
