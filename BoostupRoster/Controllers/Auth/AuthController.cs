using Asp.Versioning;
using AutoMapper;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Repositories.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Boostup.API.Controllers.Auth
{
    [ApiVersion(1)]
    // [ApiVersion(2)]
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IUserManagerRepository userManagerRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IMapper mapper;

        public AuthController(ILogger<AuthController> _logger , 
            IUserManagerRepository userManagerRepository,
            ITokenRepository tokenRepository,
            IMapper mapper
        )
        {
            this.logger = _logger;
            this.userManagerRepository = userManagerRepository;
            this.tokenRepository = tokenRepository;
            this.mapper = mapper;
        }


        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            //try
            //{
                var user = await userManagerRepository.GetUserByUserName(loginRequest.Email);
                if (user == null) {
                    logger.LogInformation("Attempt to login with invalid email: " + loginRequest.Email);
                    return BadRequest(new ApiResponse<User?>()
                    {
                        Success = false,
                        Message = "Email not found",
                        Data = user
                    });
                }

                bool passwordMatch = await userManagerRepository.CheckPassword(user, loginRequest.Password);
                if (!passwordMatch)
                {
                    logger.LogInformation("Attempt to login with invalid password by: " + loginRequest.Email);
                    return BadRequest(new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "Password Doesn't Match",
                        Data = ""
                    });
                }

                var roles = await userManagerRepository.GetRoles(user);

                if (roles == null)
                {
                    logger.LogInformation("No roles defined for user: " + loginRequest.Email);

                    return new ObjectResult(new ApiResponse<string>()
                    {
                        Success = false,
                        Message = "User doesn't have a defined role",
                        Data = ""
                    })
                    {
                        StatusCode = (int)HttpStatusCode.InternalServerError
                    };
                }

                var jwtToken = tokenRepository.CreateJWTToken(user, roles.ToList());
                var mappedUser = mapper.Map<UserResponse>(user);

                return Ok(new ApiResponse<object>()
                {
                    Success = true,
                    Message = "Login Sucessful",
                    Data = new { Token = jwtToken, Roles = roles, User = mappedUser }
                });


            /*}
            catch (Exception ex)
            {
                logger.LogInformation("Exception occured in login for: " + loginRequest.Email + "Exception :" +ex.Message);
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
