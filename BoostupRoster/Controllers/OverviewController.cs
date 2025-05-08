using Asp.Versioning;
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
    public class OverviewController : ControllerBase
    {
        private readonly ILogger<OverviewController> logger;
        private readonly IOverviewRepository overviewRepository;

        public OverviewController(ILogger<OverviewController> logger , IOverviewRepository overviewRepository)
        {
            this.logger = logger;
            this.overviewRepository = overviewRepository;
        }

        [HttpGet]
        [Route("employee/{id}")]
        [Authorize(Roles = "SuperAdmin, Employee")]
        public async Task<IActionResult> GetEmployeeOverview([FromRoute] int id)
        {
            try
            {
                var data = await overviewRepository.GetEmployeeDashboardOverview(id);
                return Ok(new ApiResponse<EmployeeOverviewResponse>()
                {
                    Data = data,
                    Success = true,
                    Message = "Employee Overview Fetched Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching employee overview " + ex.Message);
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
        [Route("admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> GetAdminOverview()
        {
            try
            {
                var data = await overviewRepository.GetAdminDashboardOverview();
                return Ok(new ApiResponse<object>()
                {
                    Data = data,
                    Success = true,
                    Message = "Admin Overview Fetched Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in fetching employee overview " + ex.Message);
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
