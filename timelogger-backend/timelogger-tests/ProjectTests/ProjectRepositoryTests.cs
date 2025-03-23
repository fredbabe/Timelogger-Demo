using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Data;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.ProjectRespository;

namespace timelogger_tests.ProjectTests
{
    public class ProjectRepositoryTests : IDisposable
    {
        private readonly AppDbContext context;
        private readonly ProjectRepository repository;

        public ProjectRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
              .UseInMemoryDatabase(databaseName: "timelogger-prod-db")
              .Options;

            context = new AppDbContext(options);
            repository = new ProjectRepository(context);
        }

        [Fact]
        public async Task CreateProject_SavesToDatabase()
        {
            // Arrange
            var project = new Project { Id = Guid.NewGuid(), Name = "Test Project", CustomerId = Guid.NewGuid(), Deadline = new DateTime().AddMonths(1) };

            // Act
            var createdProject = await repository.CreateProject(project);
            var dbProject = await context.Projects.FirstOrDefaultAsync(p => p.Id == createdProject.Id);

            // Assert
            Assert.NotNull(dbProject);
            Assert.Equal("Test Project", dbProject.Name);
            Assert.Equal(project.CustomerId, dbProject.CustomerId);
            Assert.Equal(project.Deadline, dbProject.Deadline);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsOne()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Customer" };
            var project = new Project { Id = Guid.NewGuid(), Name = "Project", CustomerId = customer.Id };
            var projects = new List<Project> { project };

            await context.Customers.AddAsync(customer);
            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetAllProjects_ReturnsTwo()
        {
            // Arrange
            var customer = new Customer { Id = Guid.NewGuid(), Name = "Customer" };
            var project1 = new Project { Id = Guid.NewGuid(), Name = "Project1", CustomerId = customer.Id };
            var project2 = new Project { Id = Guid.NewGuid(), Name = "Project2", CustomerId = customer.Id };
            var projects = new List<Project> { project1, project2 };

            await context.Customers.AddAsync(customer);
            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetAllProjects_ReturnsEmpty()
        {
            // Arrange
            var projects = new List<Project>();

            await context.Projects.AddRangeAsync(projects);
            await context.SaveChangesAsync();

            // Act
            var result = await repository.GetAllProjects();

            // Assert
            Assert.NotNull(result);
            Assert.Empty(result);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
