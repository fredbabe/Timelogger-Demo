using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Data;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.RegistrationRepository;

namespace timelogger_tests.RegistrationTests
{
    public class RegistrationRepositoryTests : IDisposable
    {
        private readonly AppDbContext context;
        private readonly RegistrationRepository registrationRepository;

        public RegistrationRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
             .UseInMemoryDatabase(databaseName: "timelogger-prod-db")
             .Options;

            context = new AppDbContext(options);
            registrationRepository = new RegistrationRepository(context);
        }

        [Fact]
        public async Task CreateRegistration_SavesToDatabase()
        {
            // Arrange
            var registration = new Registration
            {
                Id = Guid.NewGuid(),
                ProjectId = Guid.NewGuid(),
                HoursWorked = 8.5m,
                Description = "Description",
                RegistrationDate = DateTime.Now
            };

            // Act
            var createdRegistration = await registrationRepository.CreateRegistration(registration);
            var dbRegistration = await context.Registrations.FirstOrDefaultAsync(r => r.Id == createdRegistration.Id);

            // Assert
            Assert.NotNull(dbRegistration);
            Assert.Equal(registration.ProjectId, dbRegistration.ProjectId);
            Assert.Equal(registration.HoursWorked, dbRegistration.HoursWorked);
            Assert.Equal(registration.Description, dbRegistration.Description);
            Assert.Equal(registration.RegistrationDate, dbRegistration.RegistrationDate);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
