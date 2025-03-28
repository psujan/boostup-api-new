using Asp.Versioning;
using AutoMapper;
using Boostup.API.Entities;
using Boostup.API.Entities.Common;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Entities.Dtos.Response;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Repositories.Auth;
using Boostup.API.Services.Interfaces;
using Boostup.API.Templates;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Serilog.Events;
using System.Net;
using ResetPasswordRequest = Boostup.API.Entities.Dtos.Request.ResetPasswordRequest;

namespace Boostup.API.Controllers.Auth
{
    [ApiVersion(1)] 
    [Route("api/v{v:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly IUserManagerRepository userManagerRepository;
        private readonly ITokenRepository tokenRepository;
        private readonly IMapper mapper;
        private readonly IEmailService emailService;
        private readonly IConfiguration configuration;

        public AuthController(ILogger<AuthController> _logger , 
            IUserManagerRepository userManagerRepository,
            ITokenRepository tokenRepository,
            IMapper mapper,
            IEmailService emailService,
            IConfiguration configuration
        )
        {
            this.logger = _logger;
            this.userManagerRepository = userManagerRepository;
            this.tokenRepository = tokenRepository;
            this.mapper = mapper;
            this.emailService = emailService;
            this.configuration = configuration;
        }


        [MapToApiVersion(1)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] Entities.Dtos.Request.LoginRequest loginRequest)
        {
            try
            {
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


            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in login for: " + loginRequest.Email + "Exception :" +ex.Message);
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

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("reset-password-email")]
        public async Task<IActionResult> GetPasswordResetEmail([FromBody]ResetPasswordRequest request)
        {
            try
            {
                var user = await userManagerRepository.GetUserByUserName(request.Email) ?? throw new Exception("User With This Email Is Not Found");
                var resetLink = await RequestPasswordResetLink(user);
                var messageTemplate = MessageTemplates.ForgotPasswordEmailTemplate(user.FullName , resetLink);
                await emailService.SendEmailAsync(configuration["App:HostEmailAddr"], user.Email, "[BoostupCleaningService] Reset Password Instructions For Your Account", messageTemplate);
                return Ok(new ApiResponse<object>()
                {
                    Success = true,
                    Data = null,
                    Message = $"Email Send To {user.FullName} Successfully"
                });
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occured in password request for: " + request.Email + "Exception :" + ex.Message);
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

        private async Task<string> RequestPasswordResetLink(User user)
        {
           return await userManagerRepository.GetPasswordResetLink(user);
        }

        [MapToApiVersion(1)]
        [HttpPost]
        [Route("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody]UpdatePasswordReqest request)
        {
           try
            {
                var result = await userManagerRepository.UpdatePassword(request);

                if (result.Succeeded)
                {
                    return Ok(new ApiResponse<object>()
                    {
                        Success = true,
                        Data = result,
                        Message = "Password Changed Successfully"
                    });
                }
                else
                {
                    return Ok(new ApiResponse<object>()
                    {
                        Success = false,
                        Data = result,
                        Message = "Unable to update user's password"
                    });
                }
           }
           catch (Exception ex)
            {
                logger.LogError("Exception occured in updating password " + request.Email + "Exception :" + ex.Message);
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
