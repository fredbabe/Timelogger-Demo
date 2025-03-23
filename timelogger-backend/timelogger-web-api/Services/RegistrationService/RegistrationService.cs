using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Repositories.RegistrationRepository;
using timelogger_web_api.Services.ProjectService;

namespace timelogger_web_api.Services.RegistrationService
{
    public class RegistrationService : IRegistrationService
    {
        private readonly IRegistrationRepository registrationRepository;
        private readonly IProjectService projectService;

        public RegistrationService(IRegistrationRepository registrationRepository, IProjectService projectService)
        {
            this.registrationRepository = registrationRepository;
            this.projectService = projectService;
        }

        public async Task<Registration> CreateRegistration(CreateRegistrationDTORequest request)
        {

            // Verify that project isnt completed
            var project = await projectService.GetProjectById(request.ProjectId);

            if (project.IsCompleted)
            {
                throw new InvalidOperationException("Project is already completed and cannot accept new registrations.");
            }

            var currentDate = DateTime.Now;

            var registration = new Registration
            {
                ProjectId = request.ProjectId,
                CreatedOn = currentDate,
                UpdatedOn = currentDate,
                HoursWorked = request.HoursWorked,
                RegistrationDate = request.RegistrationDate,
                Description = request.Description
            };

            return await registrationRepository.CreateRegistration(registration);
        }

        public async Task<IEnumerable<GetRegistrationsForProjectResponse>> GetRegistrationOfProject(GetRegistrationsForProjectRequest request)
        {
            return await registrationRepository.GetRegistrationOfProject(request);
        }
    }
}
