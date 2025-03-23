using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using timelogger_web_api.Models.DTOs.CustomerDTOs;
using timelogger_web_api.Models.Entities;
using timelogger_web_api.Services.CustomerService;

namespace timelogger_web_api.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService customerService;
        private readonly ILogger<CustomerController> logger;

        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            this.customerService = customerService;
            this.logger = logger;
        }

        /// <summary>
        /// Creates a customer and returns the created one.
        /// </summary>
        [HttpPost("create-customer", Name = "CreateCustomer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTORequest request)
        {
            try
            {
                var customer = await customerService.CreateCustomer(request);
                return Ok(customer);
            }
            catch (ValidationException e)
            {
                logger.LogError(e, "CreateCustomerDTORequest is not valid.");
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Bad Request",
                    Detail = "CreateCustomerDTORequest is not valid."
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while creating the customer.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while creating the customer.",
                });
            }
        }

        /// <summary>
        /// Get all customers
        /// </summary>
        [HttpGet("get-all-customers", Name = "GetAllCustomers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ProblemDetails))]
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An unexpected error occurred while getting all customers.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = "An unexpected error occurred while getting all customers.",
                });
            }
        }
    }
}
