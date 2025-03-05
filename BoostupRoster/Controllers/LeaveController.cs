using Asp.Versioning;
using Azure.Core;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Employee;
using Boostup.API.Repositories.Roster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Boostup.API.Controllers
{
    [ApiVersion(1)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class LeaveController : ControllerBase
    {
        private readonly ILeaveRepository leaveRepository;
        private readonly ILogger<LeaveController> logger;

        public LeaveController(ILeaveRepository leaveRepository, ILogger<LeaveController> logger)
        {
            this.leaveRepository = leaveRepository;
            this.logger = logger;
        }

        [Authorize(Roles = "SuperAdmin, Employee")]
        [HttpPost]
        public async Task<IActionResult> AddLeave(LeaveRequest request)
        {
            try
            {
                var data = await leaveRepository.AddLeave(request);
                return Ok(new Entities.Common.ApiResponse<Leave?>()
                {
                    Success = true,
                    Message = "Leave Request Added Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in adding leave request " + ex.Message);
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
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> RemoveLeave([FromRoute]int Id)
        {
            try
            {
                var data = await leaveRepository.RemoveLeave(Id);
                return Ok(new Entities.Common.ApiResponse<Leave?>()
                {
                    Success = true,
                    Message = "Leave Request Deleted Successfully",
                    Data = data
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in deleting leave request " + ex.Message);
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
