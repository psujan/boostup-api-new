using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Boostup.API.Validations;
using Boostup.API.Entities.Dtos.Response;

namespace Boostup.API.Controllers.Roster
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class TimeSheetController : ControllerBase
    {
        private readonly ITimesheetRepository timesheetRepository;
        private readonly ILogger<TimeSheetController> logger;

        public TimeSheetController(ITimesheetRepository timesheetRepository , ILogger<TimeSheetController> logger)
        {
            this.timesheetRepository = timesheetRepository;
            this.logger = logger;
        }

        [HttpPost]
        [Route("clock-in")]
        [Authorize(Roles = "SuperAdmin ,Employee")]
        [ValidateModel]
        public async Task<IActionResult> ClockIn([FromBody]ClockInRequest request)
        {
            try
            {
                var timesheet = await timesheetRepository.Add(request);
                return Ok(new ApiResponse<Timesheet?>()
                {
                    Success = true,
                    Data = timesheet,
                    Message = "Clock in Successful"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in swapping roster " + ex.Message);
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

        [HttpPost]
        [Route("clock-out")]
        [Authorize(Roles = "SuperAdmin ,Employee")]
        [ValidateModel]
        public async Task<IActionResult> ClockOut([FromBody]ClockoutRequest request)
        {
            try
            {
                var timesheet = await timesheetRepository.Update(request);
                return Ok(new ApiResponse<Timesheet?>()
                {
                    Success = true,
                    Data = timesheet,
                    Message = "Clock Out Successful"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in swapping roster " + ex.Message);
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

        [HttpGet]
        [Route("get-paginated")]
        [Authorize (Roles ="SuperAdmin")]
        public async Task<IActionResult> GetPaginated([FromQuery] int pageNumber, int pageSize)
        {
            try
            {
                var data = await timesheetRepository.GetPaginated(pageNumber, pageSize);
                return Ok(new ApiResponse<PaginatedResponse<TimeSheetResponse>?>()
                {
                    Data = data,
                    Success = true,
                    Message = "Timesheet Record Fetched Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching paginated data of timesheet " + ex.Message);
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
