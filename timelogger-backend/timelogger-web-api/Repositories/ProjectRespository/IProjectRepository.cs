using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Repositories.ProjectRespository
{
    public interface IProjectRepository
    {
        public Task<Project> CreateProject(Project project);

        public Task<IEnumerable<Project>> GetAllProjects();

        public Task<Project?> GetProjectById(Guid projectId);

        public Task<Project?> CompleteProject(Guid projectId);

        public Task<Project?> OpenProject(Guid projectId);

        public Task DeleteProject(Guid projectId);
    }
}
