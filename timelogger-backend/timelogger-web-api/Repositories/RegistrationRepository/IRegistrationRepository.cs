using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Repositories.RegistrationRepository
{
    public interface IRegistrationRepository
    {
        public Task<Registration> CreateRegistration(Registration registration);

        public Task<IEnumerable<GetRegistrationsForProjectResponse>> GetRegistrationOfProject(GetRegistrationsForProjectRequest request);
    }
}
