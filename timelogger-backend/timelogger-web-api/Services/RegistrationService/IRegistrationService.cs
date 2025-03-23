using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Services.RegistrationService
{
    public interface IRegistrationService
    {
        public Task<Registration> CreateRegistration(CreateRegistrationDTORequest request);

        public Task<IEnumerable<GetRegistrationsForProjectResponse>> GetRegistrationOfProject(GetRegistrationsForProjectRequest request);


    }
}
