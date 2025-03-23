using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using timelogger_web_api.Controllers;
using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.RegistrationService;

namespace timelogger_tests.RegistrationTests
{
    public class RegistrationControllerTests
    {
        private readonly RegistrationController registrationController;
        private readonly Mock<IRegistrationService> registrationServiceMock;
        private readonly Mock<ILogger<RegistrationController>> loggerMock;

        public RegistrationControllerTests()
        {
            registrationServiceMock = new Mock<IRegistrationService>();
            loggerMock = new Mock<ILogger<RegistrationController>>();
            registrationController = new RegistrationController(registrationServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task CreateRegistration_ValidRequest_ReturnsCreatedRegistration()
        {
            // Arrange
            var registrationRequest = new CreateRegistrationDTORequest
            {
                ProjectId = Guid.NewGuid(),
                HoursWorked = 8.5m,
                Description = "Description",
                RegistrationDate = DateTime.Now
            };

            var createdRegistration = new Registration
            {
                Id = Guid.NewGuid(),
                ProjectId = registrationRequest.ProjectId,
                HoursWorked = registrationRequest.HoursWorked,
                Description = registrationRequest.Description,
                RegistrationDate = registrationRequest.RegistrationDate
            };

            registrationServiceMock
                .Setup(x => x.CreateRegistration(registrationRequest))
                .ReturnsAsync(new Registration
                {
                    Id = Guid.NewGuid(),
                    ProjectId = registrationRequest.ProjectId,
                    HoursWorked = registrationRequest.HoursWorked,
                    Description = registrationRequest.Description,
                    RegistrationDate = registrationRequest.RegistrationDate
                });

            // Act
            var result = await registrationController.CreateRegistration(registrationRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedRegistration = Assert.IsType<Registration>(okResult.Value);

            Assert.NotEqual(Guid.Empty, returnedRegistration.Id);
            Assert.Equal(registrationRequest.ProjectId, returnedRegistration.ProjectId);
            Assert.Equal(registrationRequest.HoursWorked, returnedRegistration.HoursWorked);
            Assert.Equal(registrationRequest.Description, returnedRegistration.Description);
            Assert.Equal(registrationRequest.RegistrationDate, returnedRegistration.RegistrationDate);
        }
    }
}