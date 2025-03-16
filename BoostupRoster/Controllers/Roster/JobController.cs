using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("{id}/list-employees")]
        public async Task<IActionResult> GetEmployeeeByJob([FromRoute] int Id)
        {
            try
            {
                var data = await jobRepository.ListEmployeeByJob(Id);
                return Ok(new Entities.Common.ApiResponse<IEnumerable<EmployeeBasicResponse>?>()
                {
                    Success = true,
                    Message = "Employee For The Requested Job Fetched Successfully",
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

        [Authorize (Roles ="SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] JobRequest request)
        {
            try
            {
                var row = await jobRepository.Add(new Jobs()
                {
                    Title = request.Title,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Notes = request.Notes,
                    JobAddress = request.JobAddress,
                });
                return Ok(new ApiResponse<Jobs>()
                {
                    Data = row,
                    Message = "Job Added Successfully",
                    Success = true
                });
            }
            catch(Exception ex)
            {
                logger.LogError("Exception occured in adding job " + ex.Message);
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] JobRequest request)
        {
            try
            {
                var row = await jobRepository.Update(id, request);
                return Ok(new ApiResponse<Jobs>()
                {
                    Data = row,
                    Message = "Job Updated Successfully",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in updating job " + ex.Message);
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var row = await jobRepository.Delete(id);
                return Ok(new ApiResponse<Jobs>()
                {
                    Data = row,
                    Message = "Job Deleted Successfully",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in deleteing job " + ex.Message);
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

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        [Route("get-paginated")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageNumber, int pageSize)
        {

             try
             {
                var rows = await jobRepository.GetPaginated(pageNumber, pageSize);
                 return Ok(new ApiResponse<PaginatedResponse<Jobs>?>()
                 {
                     Data = rows,
                     Message = "Jobs Fetched Successfully",
                     Success = true
                 });
             }
             catch (Exception ex)
             {
                 logger.LogError("Exception occured in deleteing job " + ex.Message);
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
