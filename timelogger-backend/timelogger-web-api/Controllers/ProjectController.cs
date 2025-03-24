using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using timelogger_web_api.Models.DTOs.ProjectDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.ProjectService;

namespace timelogger_web_api.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(IProjectService projectService, ILogger<ProjectController> logger)
        {
            this.projectService = projectService;
            this.logger = logger;
        }


        /// <summary>
        /// Creates a project and returns the created one.
        /// </summary>
        [HttpPost("create-project", Name = "CreateProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]

        public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTORequest request)
        {
            try
            {
                var project = await projectService.CreateProject(request);
                return Ok(project);
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
                logger.LogError(e, "CreateProjectDTORequest is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "CreateProjectDTORequest is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while creating the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while creating the project.",
                });
            }
        }

        /// <summary>
        /// Retrieves all projects.
        /// </summary>
        [HttpGet("get-all-projects", Name = "GetAllProjects")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Project>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllProjects()
        {
            try
            {
                var projects = await projectService.GetAllProjects();
                return Ok(projects);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while getting all projects.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while getting all projects.",
                });
            }
        }

        /// <summary>
        /// Complete project.
        /// </summary>
        [HttpPatch("complete-project", Name = "CompleteProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CompleteProject([FromQuery] Guid projectId)
        {
            try
            {
                var project = await projectService.CompleteProject(projectId);
                return Ok(project);
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
                logger.LogError(e, "ProjectId is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "ProjectId is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while completing the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while completing the project.",
                });
            }
        }

        /// <summary>
        /// Open project.
        /// </summary>
        [HttpPatch("open-project", Name = "OpenProject")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Project))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> OpenProject([FromQuery] Guid projectId)
        {
            try
            {
                var project = await projectService.OpenProject(projectId);
                return Ok(project);
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
                logger.LogError(e, "ProjectId is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "ProjectId is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while opening the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while opening the project.",
                });
            }
        }

        /// <summary>
        /// Delete project.
        /// </summary>
        [HttpDelete("delete-project", Name = "DeleteProject")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> DeleteProject([FromQuery] Guid projectId)
        {
            try
            {
                await projectService.DeleteProject(projectId);
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
                logger.LogError(e, "ProjectId is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "ProjectId is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while deleting the project.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while deleting the project.",
                });
            }
        }

    }
}
