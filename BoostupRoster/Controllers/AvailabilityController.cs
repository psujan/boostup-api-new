using Asp.Versioning;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Employee;
using Boostup.API.Repositories.Employee;
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
       

        // GET api/<AvailabilityController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AvailabilityController>
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Post(AvailabilityRequest request) 
        {
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
            /*try
            {
                var row = await availabilityRepository.Add(new EmployeeAvailability()
                {
                    EmployeeId = request.EmployeeId,
                    ForFullDay = request.ForFullDay,
                    From = request.From,
                    To = request.To,
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
                logger.LogError("Exception occured in fetching paginated data " + ex.Message);
                return new ObjectResult(new ApiResponse<string>()
                {
                    Success = false,
                    Message = ex.Message,
                    Data = ""
                })
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }*/
        }
    }
}
