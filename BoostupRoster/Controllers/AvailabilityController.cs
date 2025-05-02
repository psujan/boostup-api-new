using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
//using Boostup.API.Repositories.Employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Boostup.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AvailabilityController : ControllerBase
    {
        private readonly IAvailabilityRepository availabilityRepository;
        private readonly ILogger logger;

        public AvailabilityController(IAvailabilityRepository availabilityRepository , ILogger<AvailabilityController> logger)
        {
            this.availabilityRepository = availabilityRepository;
            this.logger = logger;
        }


        // POST api/<AvailabilityController>
        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpPost]
        public async Task<IActionResult> Post(AvailabilityRequest request) 
        {
            try
            {
                var isExist = await availabilityRepository.FindAvailability(request.EmployeeId, request.From, request.To);
                if(isExist != null)
                {
                    throw new Exception("An availability record already exist: " + request.From + " to " + request.To);
                }

                var count = await availabilityRepository.GetTotalDayCount(request.EmployeeId, request.Day);

                if (count >= 3)
                {
                    throw new Exception("There are already 3 availability records for "+ request.Day +".The max limit is 3");

                }

                var row = await availabilityRepository.Add(new EmployeeAvailability()
                {
                    EmployeeId = request.EmployeeId,
                    ForFullDay = request.ForFullDay,
                    From = request.From,
                    To = request.To,
                    Day = request.Day,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                });
                return Ok(new ApiResponse<EmployeeAvailability?>()
                {
                    Success = true,
                    Data = row,
                    Message = "Availability Added Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in adding availability" + ex.Message);
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

        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id , [FromBody] AvailabilityRequest request)
        {
            try
            {
                var row = await availabilityRepository.Update(id, request);
                return Ok(new ApiResponse<EmployeeAvailability?>()
                {
                    Success = true,
                    Data = row,
                    Message = "Availability Updated Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in updating availability" + ex.Message);
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

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var row = await availabilityRepository.Delete(id);
                return Ok(new ApiResponse<EmployeeAvailability?>()
                {
                    Success = true,
                    Data = row,
                    Message = "Availability Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in deleting availability " + ex.Message);
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

        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpGet]
        [Route("employee/{id}")]
        public async Task<IActionResult> GetEmployeeAvailability([FromRoute] int id)
        {
            try
            {
                var rows = await availabilityRepository.GroupEmployeeAvailability(id);
                return Ok(new ApiResponse<List<GroupedAvailabilityResponse>?>()
                {
                    Success = true,
                    Data = rows,
                    Message = "Availability Fetched Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching employee availability " + ex.Message);
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
