using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Roster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Boostup.API.Controllers.Roster
{
    [ApiVersion(1)]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private readonly IRosterRepository rosterRepository;

        public RosterController(IRosterRepository rosterRepository)
        {
            this.rosterRepository = rosterRepository;
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpPost]
        public async Task<IActionResult> AddRoster([FromBody] RosterRequest[] requests)
        {
            var roster = await rosterRepository.AddRoster(requests.ToList());
            return Ok(new ApiResponse<List<Entities.Roster>?>()
            {
                Success = true,
                Message = "Roster Added Successfully",
                Data = roster
            });
        }

        [Authorize(Roles = "SuperAdmin")]
        [HttpGet]
        public async Task<IActionResult> ListRoster([FromQuery] RosterFilterRequest request)
        {
            var rows = await rosterRepository.ListRoster(request);
            return Ok(new ApiResponse<IEnumerable<EmployeeWithRosterResponse>>()
            {
                Success = true,
                Data = rows,
                Message = "Data Fetched Successfully"
            });
        }
    }
}
