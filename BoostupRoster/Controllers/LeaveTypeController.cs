using Asp.Versioning;
using Azure.Core;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Boostup.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly ILeaveTypeRepository leaveTypeRepository;
        private readonly ILogger<LeaveTypeController> logger;

        public LeaveTypeController(ILeaveTypeRepository leaveTypeRepository , ILogger<LeaveTypeController> logger)
        {
            this.leaveTypeRepository = leaveTypeRepository;
            this.logger = logger;
        }

        [HttpGet]
        [Authorize (Roles ="SuperAdmin , Employee")]
        public async Task<IActionResult> GetList()
        {
            try
            {
                var data = await leaveTypeRepository.GetAll();
                return Ok(new Entities.Common.ApiResponse<IEnumerable<LeaveType>>()
                {
                    Success = true,
                    Message = "Data Fetched Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching leave list " + ex.Message);
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
