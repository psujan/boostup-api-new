using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Boostup.API.Controllers.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class RosterController : ControllerBase
    {
        private readonly ILogger<RosterController> logger;
        private readonly IMapper mapper;

        public RosterController(ILogger<RosterController> logger , IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
        }
    }
}
