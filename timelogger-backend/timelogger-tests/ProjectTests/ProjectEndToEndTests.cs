using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Json;
using timelogger_tests.EndToEndTests;
using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_tests.ProjectTests
{
    public class ProjectEndToEndTests
    : IClassFixture<CustomWebApplicationFactory>
    {
        private readonly HttpClient _client;
        private readonly CustomWebApplicationFactory _factory;

        public ProjectEndToEndTests(CustomWebApplicationFactory factory)
        {
            // Spin up an in-memory test server
            _client = factory.CreateClient();
            _factory = factory;
        }

        [Fact]
        public async Task CreateProject_InvalidRequest_ReturnsBadRequest()
        {
            // Arrange
            var invalidRequest = new CreateProjectDTORequest
            {
                Name = "",
                CustomerId = Guid.Empty,

            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/projects/create-project", invalidRequest);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var problem = await response.Content.ReadFromJsonAsync<ProblemDetails>();
            Assert.NotNull(problem);
        }

        [Fact]
        public async Task CreateProject_ValidRequest_ReturnsCreatedProject()
        {
            // Arrange
            var projectRequest = new CreateProjectDTORequest
            {
                Name = "New Project",
                CustomerId = _factory.SeededCustomerId,
                Deadline = new DateTime().AddMonths(1)
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/projects/create-project", projectRequest);

            // Assert
            response.EnsureSuccessStatusCode();

            var createdProject = await response.Content.ReadFromJsonAsync<Project>();
            Assert.NotNull(createdProject);
            Assert.NotEqual(Guid.Empty, createdProject.Id);
            Assert.Equal(projectRequest.Name, createdProject.Name);
            Assert.Equal(projectRequest.CustomerId, createdProject.CustomerId);
            Assert.Equal(projectRequest.Deadline, createdProject.Deadline);
        }
    }
}

