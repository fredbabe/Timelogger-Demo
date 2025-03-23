using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Data;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.CustomerRepository;

namespace timelogger_tests.CustomerTests
{
    public class CustomerRepositoryTests : IDisposable
    {
        private readonly AppDbContext context;
        private readonly CustomerRepository repository;

        public CustomerRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "timelogger-prod-db")
              .Options;

            context = new AppDbContext(options);
            repository = new CustomerRepository(context);
        }

        [Fact]
        public async Task CreateCustomer_SavesToDatabse()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Test Customer" };

            // Act
            var createdCustomer = await repository.CreateCustomer(customer);
            var dbCustomer = await context.Customers.FirstOrDefaultAsync(c => c.Id == createdCustomer.Id);

            // Assert
            Assert.NotNull(dbCustomer);
            Assert.Equal("Test Customer", dbCustomer.Name);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
