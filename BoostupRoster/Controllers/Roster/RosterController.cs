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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boostup.API.Controllers.Roster
{
    [ApiVersion(1)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private readonly IRosterRepository rosterRepository;
        private readonly ILogger<RosterController> logger;

        public RosterController(IRosterRepository rosterRepository, ILogger<RosterController> logger)
        {
            this.rosterRepository = rosterRepository;
            this.logger = logger;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddRoster([FromBody] RosterRequest[] requests)
        {
            try
            {
                if (requests.Length == 0)
                {
                    throw new Exception("Request is empty. Please provide valid request");
                }
                var roster = await rosterRepository.AddRoster(requests.ToList());
                return Ok(new ApiResponse<List<Entities.Roster>?>()
                {
                    Success = true,
                    Message = "Roster Added Successfully",
                    Data = roster
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in adding roster " + ex.Message);
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
        public async Task<IActionResult> ListRoster([FromQuery] RosterFilterRequest request)
        {
            try
            {
                var rows = await rosterRepository.ListRoster(request);
                return Ok(new ApiResponse<PaginatedResponse<EmployeeWithRosterResponse?>?>()
                {
                    Success = true,
                    Data = rows,
                    Message = "Data Fetched Successfully"
                });
            }
            catch (Exception ex) 
            {
                logger.LogError("Exception occured in fetching rosters " + ex.Message);
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
        [HttpPost]
        [Route("swap")]
        public async Task<IActionResult> SwapRoster([FromBody] RosterSwapRequest request)
        {
            try
            {
                var data = await rosterRepository.SwapRoster(request);
                return Ok(new ApiResponse<Entities.Roster>()
                {
                    Success = true,
                    Data = data,
                    Message = "Roster Swap Successfully"
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

        [Authorize(Roles = "SuperAdmin,Employee")]
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetRosterById([FromRoute]int id)
        {
            try
            {
                var row = await rosterRepository.GetById(id);
                return Ok(new ApiResponse<RosterResponse?>()
                {
                    Success = true,
                    Data = row,
                    Message = "Roster Fetched Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in deleting roster " + ex.Message);
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
        public async Task<IActionResult> DeleteRoster([FromRoute] int id)
        {
            try
            {
                var row = await rosterRepository.DeleteRoster(id);
                return Ok(new ApiResponse<Entities.Roster?>()
                {
                    Success = true,
                    Data = row,
                    Message = "Roster Deleted Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in deleting roster " + ex.Message);
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
