using Moq;
using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.ProjectRespository;
using timelogger_web_api.Services.ProjectService;

namespace timelogger_tests.ProjectTests
{
    public class ProjectServiceTests
    {
        private readonly IProjectService projectService;
        private readonly Mock<IProjectRepository> projectRepositoryMock;

        public ProjectServiceTests()
        {
            projectRepositoryMock = new Mock<IProjectRepository>();
            projectService = new ProjectService(projectRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateProject_ReturnsProject()
        {
            // Arrange
            var request = new CreateProjectDTORequest { Name = "Valid Project", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(2) };
            var createdProject = new Project { Id = Guid.NewGuid(), Name = request.Name, CustomerId = request.CustomerId, Deadline = request.Deadline };

            projectRepositoryMock
                .Setup(repo => repo.CreateProject(It.IsAny<Project>()))
                .ReturnsAsync(createdProject);

            // Act
            var result = await projectService.CreateProject(request);

            // Assert
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.CustomerId, result.CustomerId);
            Assert.Equal(request.Deadline, result.Deadline);
            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsOne()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Project", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var projects = new List<Project> { project };

            projectRepositoryMock
                .Setup(repo => repo.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectService.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsTwo()
        {
            // Arrange
            var project1 = new Project { Id = Guid.NewGuid(), Name = "Project1", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var project2 = new Project { Id = Guid.NewGuid(), Name = "Project2", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };
            var projects = new List<Project> { project1, project2 };

            projectRepositoryMock
                .Setup(repo => repo.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectService.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllProjetcs_ReturnsEmpty()
        {
            // Arrange
            var projects = new List<Project>();

            projectRepositoryMock
                .Setup(repo => repo.GetAllProjects())
                .ReturnsAsync(projects);

            // Act
            var result = await projectService.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }
    }
}
