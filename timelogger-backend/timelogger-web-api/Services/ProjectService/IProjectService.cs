using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Services.ProjectService
{
    public interface IProjectService
    {
        public Task<Project> CreateProject(CreateProjectDTORequest request);

        public Task<IEnumerable<Project>> GetAllProjects();

        public Task<GetProjectDTOResponse> GetProjectById(Guid projectId);

        public Task<Project?> CompleteProject(Guid projectId);

        public Task<Project?> OpenProject(Guid projectId);

        public Task DeleteProject(Guid projectId);
    }
}
