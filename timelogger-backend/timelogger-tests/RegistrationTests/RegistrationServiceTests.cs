using Moq;
using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.RegistrationRepository;
using timelogger_web_api.Services.ProjectService;
using timelogger_web_api.Services.RegistrationService;

namespace timelogger_tests.RegistrationTests
{
    public class RegistrationServiceTests
    {
        private readonly IRegistrationService registrationService;
        private readonly Mock<IRegistrationRepository> registrationRepositoryMock;
        private readonly Mock<IProjectService> projectServiceMock;

        public RegistrationServiceTests()
        {
            registrationRepositoryMock = new Mock<IRegistrationRepository>();
            projectServiceMock = new Mock<IProjectService>();
            registrationService = new RegistrationService(registrationRepositoryMock.Object, projectServiceMock.Object);
        }

        [Fact]
        public async Task CreateRegistration_ReturnsRegistration()
        {
            // Arrange
            var request = new CreateRegistrationDTORequest
            {
                ProjectId = Guid.NewGuid(),
                HoursWorked = 8.5m,
                Description = "Description",
                RegistrationDate = DateTime.Now
            };

            var createdRegistration = new Registration
            {
                Id = Guid.NewGuid(),
                ProjectId = request.ProjectId,
                HoursWorked = request.HoursWorked,
                Description = request.Description,
                RegistrationDate = request.RegistrationDate
            };

            var project = new GetProjectDTOResponse { IsCompleted = false, Name = "Project Name", };

            registrationRepositoryMock
                .Setup(repo => repo.CreateRegistration(It.IsAny<Registration>()))
                .ReturnsAsync(createdRegistration);

            projectServiceMock
                .Setup(service => service.GetProjectById(It.IsAny<Guid>()))
                .ReturnsAsync(project);

            // Act
            var result = await registrationService.CreateRegistration(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(request.ProjectId, result.ProjectId);
            Assert.Equal(request.HoursWorked, result.HoursWorked);
            Assert.Equal(request.Description, result.Description);
            Assert.Equal(request.RegistrationDate, result.RegistrationDate);
            Assert.NotEqual(Guid.Empty, result.Id);
        }
    }
}
