using Microsoft.EntityFrameworkCore;
using timelogger_web_api.Data;
using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;

namespace timelogger_web_api.Repositories.RegistrationRepository
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly AppDbContext context;

        public RegistrationRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Registration> CreateRegistration(Registration registration)
        {
            await context.Registrations.AddAsync(registration);
            await context.SaveChangesAsync();

            return registration;
        }

        public async Task<IEnumerable<GetRegistrationsForProjectResponse>> GetRegistrationOfProject(GetRegistrationsForProjectRequest request)
        {
            var registrations = await context.Registrations
                .Where(r => r.ProjectId == request.ProjectId)
                .Select(r => new GetRegistrationsForProjectResponse
                {
                    Id = r.Id,
                    HoursWorked = r.HoursWorked,
                    Description = r.Description,
                    RegistrationDate = r.RegistrationDate
                })
                .ToListAsync();

            return registrations;
        }
    }
}
