using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.ProjectRespository;

namespace timelogger_web_api.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            this.projectRepository = projectRepository;
        }

        public async Task<Project> CreateProject(CreateProjectDTORequest request)
        {
            var currentDate = DateTime.Now;

            var project = new Project
            {
                Name = request.Name,
                Description = request.Description,
                CreatedOn = currentDate,
                UpdatedOn = currentDate,
                CustomerId = request.CustomerId,
                Deadline = request.Deadline
            };

            return await projectRepository.CreateProject(project);
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await projectRepository.GetAllProjects();
        }

        public async Task<GetProjectDTOResponse> GetProjectById(Guid projectId)
        {
            var result = await projectRepository.GetProjectById(projectId);

            var response = new GetProjectDTOResponse
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedOn = result.CreatedOn,
                UpdatedOn = result.UpdatedOn,
                CustomerId = result.CustomerId,
                Deadline = result.Deadline,
            };

            return response;
        }

        public async Task<Project?> CompleteProject(Guid projectId)
        {
            var project = await projectRepository.CompleteProject(projectId);

            return project;
        }

        public async Task<Project?> OpenProject(Guid projectId)
        {
            var project = await projectRepository.OpenProject(projectId);

            return project;
        }

        public async Task DeleteProject(Guid projectId)
        {
            await projectRepository.DeleteProject(projectId);
        }
    }
}
