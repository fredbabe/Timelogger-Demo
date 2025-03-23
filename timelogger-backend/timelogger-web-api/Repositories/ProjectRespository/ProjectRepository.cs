using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Data;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Repositories.ProjectRespository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext context;

        public ProjectRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Project> CreateProject(Project project)
        {
            await context.Projects.AddAsync(project);
            await context.SaveChangesAsync();

            return project;
        }

        public async Task<IEnumerable<Project>> GetAllProjects()
        {
            return await context.Projects
                .Include(p => p.Customer)
                .ToListAsync();
        }

        public async Task<Project?> GetProjectById(Guid projectId)
        {
            return await context.Projects.FindAsync(projectId);
        }

        public async Task<Project?> CompleteProject(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);

            if (project != null)
            {
                project.IsCompleted = true;

                await context.SaveChangesAsync();

                return project;
            }

            return null;
        }

        public async Task<Project?> OpenProject(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);

            if (project != null)
            {
                project.IsCompleted = false;

                await context.SaveChangesAsync();

                return project;
            }

            return null;
        }

        public async Task DeleteProject(Guid projectId)
        {
            var project = await context.Projects.FindAsync(projectId);

            if (project != null)
            {
                context.Projects.Remove(project);
                await context.SaveChangesAsync();
            }
        }
    }
}
