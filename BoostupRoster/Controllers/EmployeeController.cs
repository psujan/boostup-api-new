using Asp.Versioning;
using AutoMapper;
using Boostup.API.Data;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Boostup.API.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace Boostup.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IUserManagerRepository userManagerRepository;
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepository;
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ILogger<EmployeeController> logger,
            IUserManagerRepository userManagerRepository,
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            ApplicationDbContext dbContext
        )
        {
            this.logger = logger;
            this.userManagerRepository = userManagerRepository;
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
            this.dbContext = dbContext;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("onboard")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> OnboardEmployee(OnboardRequest request)
        {
            try
            {
                var user = await userManagerRepository.RegisterUser(request);
                if (user == null)
                {
                    return BadRequest(new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "Registration Failed At The Moment",
                        Data = ""
                    });
                }

                var employee = await employeeRepository.AddEmployee(user, request.Phone);

                if (employee == null)
                {
                    logger.LogCritical("User Registered But Failed To Create Employee Profile: " + user.Id);
                }

                return Ok(new ApiResponse<EmployeeDetailResponse>()
                {
                    Success = true,
                    Message = "Employee " + user.FullName + " is successfully onboarded",
                    Data = employee
                });

            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in onboarding " + ex.Message);
                return new ObjectResult(new ApiResponse<string>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ""
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Route("{id}")]
        [Authorize(Roles = "SuperAdmin ,Employee")]
        public async Task<IActionResult> GetEmployeeDetail([FromRoute] int id)
        {
            try
            {
                var employee = await employeeRepository.GetById(id);
                return Ok(new ApiResponse<EmployeeDetailResponse>()
                {
                    Success = true,
                    Message = "Data Fetched Successfullu",
                    Data = employee
                });
            }
            catch (Exception ex)
            {

                logger.LogError("Exception occured in fetching detail " + ex.Message);
                return new ObjectResult(new ApiResponse<string>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ""
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };

            }
        }

        [MapToApiVersion(1)]
        [HttpGet]
        [Route("paginated")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageNumber, int pageSize)
        {
            try
            {
                var data = await employeeRepository.GetPaginated(pageNumber, pageSize);
                return Ok(new ApiResponse<PaginatedResponse<EmployeeDetailResponse?>>()
                {
                    Success = true,
                    Message = "Data Fetched Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching paginated data " + ex.Message);
                return new ObjectResult(new ApiResponse<string>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ""
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
        }
    }
}
