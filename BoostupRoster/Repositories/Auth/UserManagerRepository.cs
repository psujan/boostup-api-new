using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Boostup.API.Interfaces.Auth;
using Boostup.API.Services;
using Microsoft.AspNetCore.Identity;

namespace Boostup.API.Repositories.Auth
{
    public class UserManagerRepository : IUserManagerRepository
    {
        private readonly UserManager<User> userManager;
        private readonly IConfiguration configuration;
        private static readonly string DefaultRole = "Employee";
        private static readonly string ResetLink = "/reset-password";
        public UserManagerRepository(UserManager<User> userManager, IConfiguration configuration)
        {
            this.userManager = userManager;
            this.configuration = configuration;
        }

        public async Task<User?> RegisterUser(OnboardRequest request)
        {
            var user = new User
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName
            };
            var password =  Helper.GenerateRandomString(5);
            var identityResult = await userManager.CreateAsync(user, password);

            if (!identityResult.Succeeded)
            {
                return null;
            }

            // Add Role to User
            identityResult = await userManager.AddToRoleAsync(user, DefaultRole);

            if (!identityResult.Succeeded)
            {
                return null;
            }
            /*
             * TODO 
             * Send Email To The Registered User Mentioning Its Email And Password
             */
            return user;

        }

        public async Task<Boolean> CheckPassword(User user, string password)
        {
            bool passwordMatch = await userManager.CheckPasswordAsync(user, password);
            return passwordMatch;
        }

        public async Task<List<string>?> GetRoles(User user)
        {
            var roles = await userManager.GetRolesAsync(user);

            if (roles == null)
            {
                return null;
            }

            return roles.ToList();
        }

        public async Task<User?> GetUserByUserName(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return user;
        }

        public async Task<User?> GetUserById(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            return user;
        }

        public async Task<string> GetPasswordResetLink(User user)
        {
            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            if(configuration["App:Env"] == "Development")
            {
                return configuration["App:FrontendBaseUrlDevelopment"] + $"?email={user.Email}&token={resetToken}";
            }

            return configuration["App:FrontendBaseUrlProduction"] + $"?email={user.Email}&token={resetToken}";

        }

        public async Task<IdentityResult> UpdatePassword(UpdatePasswordReqest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email) ?? throw new Exception("User Not Found");
            var result = await userManager.ResetPasswordAsync(user, request.ResetToken, request.Password);
            return result;
        }
    }
}
