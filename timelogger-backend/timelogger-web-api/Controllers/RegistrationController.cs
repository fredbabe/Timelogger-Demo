using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using timelogger_web_api.Models.DTOs.RegistrationDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.RegistrationService;

namespace timelogger_web_api.Controllers
{
    [ApiController]
    [Route("api/registrations")]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegistrationService registrationService;
        private readonly ILogger<RegistrationController> logger;


        public RegistrationController(IRegistrationService registrationService, ILogger<RegistrationController> logger)
        {
            this.registrationService = registrationService;
            this.logger = logger;
        }

        /// <summary>
        /// Get all registrations of a project.
        /// </summary>
        [HttpGet("get-registrations-of-project", Name = "GetRegistrationsOfProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetRegistrationsForProjectResponse>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetRegistrationOfProject([FromQuery] GetRegistrationsForProjectRequest request)
        {
            try
            {
                var registrations = await registrationService.GetRegistrationOfProject(request);
                return Ok(registrations);
            }
            catch (ValidationException e)
            {
                logger.LogError(e, "GetRegistrationsForProjectRequest is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "GetRegistrationsForProjectRequest is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while getting the registrations.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while getting the registrations."
                });
            }
        }

        /// <summary>
        /// Creates a registration on a project and returns the created one.
        /// </summary>
        [HttpPost("create-registration", Name = "CreateRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Registration))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]

        public async Task<IActionResult> CreateRegistration([FromBody] CreateRegistrationDTORequest request)
        {
            try
            {
                var registration = await registrationService.CreateRegistration(request);
                return Ok(registration);
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e, "A database constraint violation occurred");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "A data validation error occurred. Please check your input and try again."
                });
            }
            catch (ValidationException e)
            {
                logger.LogError(e, "CreateRegistrationDTORequest is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "CreateRegistrationDTORequest is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while creating the registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while creating the registration."
                });
            }
        }

        ///// <summary>
        ///// Delete registration
        ///// </summary>
        [HttpDelete("delete-registration", Name = "DeleteRegistration")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteRegistration([FromQuery] Guid registrationId)
        {
            try
            {
                await registrationService.DeleteRegistration(registrationId);
                return Ok();
            }
            catch (DbUpdateException e)
            {
                logger.LogError(e, "A database constraint violation occurred");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "A data validation error occurred. Please check your input and try again."
                });
            }
            catch (ValidationException e)
            {
                logger.LogError(e, "RegistrationId is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "RegistrationId is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while deleting the registration.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while deleting the registration.",
                });
            }
        }
    }
}
