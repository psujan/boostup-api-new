using Asp.Versioning;
using AutoMapper;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Interfaces.Employee;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;

namespace Boostup.API.Controllers.Admin
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

        public EmployeeController(ILogger<EmployeeController> logger , 
            IUserManagerRepository userManagerRepository , 
            IMapper mapper,
            IEmployeeRepository employeeRepository
        )
        {
            this.logger = logger;
            this.userManagerRepository = userManagerRepository;
            this.mapper = mapper;
            this.employeeRepository = employeeRepository;
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("onboard")]
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

                return Ok(new ApiResponse<string>()
                {
                    Success = true,
                    Message = "Employee " + user.FullName +" is successfully onboarded",
                    Data = "Onboard Successful"
                });

            }catch (Exception ex) {
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

    }
}
