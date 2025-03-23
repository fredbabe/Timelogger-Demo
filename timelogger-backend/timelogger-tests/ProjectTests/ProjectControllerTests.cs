using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using timelogger_web_api.Controllers;
using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.ProjectService;

namespace timelogger_tests.ProjectTests
{
    public class ProjectControllerTests
    {
        private readonly ProjectController projectController;
        private readonly Mock<IProjectService> projectServiceMock;
        private readonly Mock<ILogger<ProjectController>> loggerMock;

        public ProjectControllerTests()
        {
            projectServiceMock = new Mock<IProjectService>();
            loggerMock = new Mock<ILogger<ProjectController>>();
            projectController = new ProjectController(projectServiceMock.Object, loggerMock.Object);
        }

        [Fact]
        public async Task CreateProject_ValidRequest_ReturnsCreatedProject()
        {
            // Arrange
            var projectRequest = new CreateProjectDTORequest { Name = "New Project", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var createdProject = new Project { Id = Guid.NewGuid(), Name = "New Project", CustomerId = projectRequest.CustomerId, Deadline = projectRequest.Deadline };

            projectServiceMock
                .Setup(x => x.CreateProject(projectRequest))
                .ReturnsAsync(new Project { Id = Guid.NewGuid(), Name = projectRequest.Name, CustomerId = projectRequest.CustomerId, Deadline = projectRequest.Deadline });

            // Act
            var result = await projectController.CreateProject(projectRequest);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProject = Assert.IsType<Project>(okResult.Value);

            Assert.NotEqual(Guid.Empty, returnedProject.Id);
            Assert.Equal(projectRequest.Name, returnedProject.Name);
            Assert.Equal(projectRequest.CustomerId, returnedProject.CustomerId);
            Assert.Equal(projectRequest.Deadline, returnedProject.Deadline);
        }


        [Fact]
        public async Task GetAllProjects_ReturnsTwo()
        {
            // Arrange
            var project1 = new Project { Id = Guid.NewGuid(), Name = "Project1", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var project2 = new Project { Id = Guid.NewGuid(), Name = "Project2", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(2) };
            var projects = new List<Project> { project1, project2 };

            projectServiceMock
                .Setup(x => x.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectController.GetAllProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProjects = Assert.IsType<List<Project>>(okResult.Value);

            Assert.Equal(2, returnedProjects.Count);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsOne()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Project", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var projects = new List<Project> { project };

            projectServiceMock
                .Setup(x => x.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectController.GetAllProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProjects = Assert.IsType<List<Project>>(okResult.Value);

            Assert.Single(returnedProjects);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsEmpty()
        {
            // Arrange
            var projects = new List<Project>();

            projectServiceMock
                .Setup(x => x.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectController.GetAllProjects();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProjects = Assert.IsType<List<Project>>(okResult.Value);
            Assert.Empty(returnedProjects);
        }
    }
}
