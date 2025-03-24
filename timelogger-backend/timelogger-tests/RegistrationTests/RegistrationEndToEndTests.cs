using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Json;
using timelogger_tests.EndToEndTests;
using timelogger_web_api.Data;
using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_tests.RegistrationTests
{
    public class RegistrationEndToEndTests
    : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;


        public RegistrationEndToEndTests(CustomWebApplicationFactory factory)
        {
            // Spin up an in-memory test server
            _client = factory.CreateClient();
            _factory = factory;
            ResetDatabase();
        }

        private void ResetDatabase()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // Clear existing data
                db.Customers.RemoveRange(db.Customers);
                db.Projects.RemoveRange(db.Projects);
                db.SaveChanges();

                // Re-seed required data
                _factory.SeededCustomerId = Guid.NewGuid();
                _factory.SeededProjectId = Guid.NewGuid();

                db.Customers.Add(new Customer
                {
                    Id = _factory.SeededCustomerId,
                    Name = "Test Customer"
                });

                db.Projects.Add(new Project
                {
                    Id = _factory.SeededProjectId,
                    Name = "Test Project",
                    CustomerId = _factory.SeededCustomerId
                });

                db.SaveChanges();
            }
        }

        [Fact]
        public async Task CreateRegistration_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var invalidRequest = new CreateRegistrationDTORequest
            {
                ProjectId = Guid.Empty,
                HoursWorked = 0,
                Description = "",
                RegistrationDate = DateTime.Now
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/registrations/create-registration", invalidRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
        }

        [Fact]
        public async Task CreateRegistration_ValidRequest_ReturnsCreatedRegistration()
        {
            // Arrange
            var registrationRequest = new CreateRegistrationDTORequest
            {
                ProjectId = _factory.SeededProjectId,
                HoursWorked = 8.5m,
                Description = "Description",
                RegistrationDate = DateTime.Now
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/registrations/create-registration", registrationRequest);

            // Assert
            response.EnsureSuccessStatusCode();

            var createdRegistration = await response.Content.ReadFromJsonAsync<Registration>();

            Assert.NotNull(createdRegistration);
            Assert.NotEqual(Guid.Empty, createdRegistration.Id);
            Assert.Equal(registrationRequest.ProjectId, createdRegistration.ProjectId);
            Assert.Equal(registrationRequest.HoursWorked, createdRegistration.HoursWorked);
            Assert.Equal(registrationRequest.Description, createdRegistration.Description);
            Assert.Equal(registrationRequest.RegistrationDate, createdRegistration.RegistrationDate);
        }
    }
}
