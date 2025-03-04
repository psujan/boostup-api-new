using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Roster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Boostup.API.Controllers.Roster
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobRepository jobRepository;
        private readonly ILogger logger;

        public JobController(IJobRepository jobRepository, ILogger<JobController> logger)
        {
            this.jobRepository = jobRepository;
            this.logger = logger;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        [Route("add-employees")]
        public async Task<IActionResult> AddEmployeeToJob([FromBody] JobEmployeeRequest request)
        {
            try
            {
                var data = await jobRepository.AddEmployeeToJob(request);
                return Ok(new Entities.Common.ApiResponse<IEnumerable<JobEmployee>?>()
                {
                    Success = true,
                    Message = "Employee Added To Job Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in adding employee to jobs " + ex.Message);
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
