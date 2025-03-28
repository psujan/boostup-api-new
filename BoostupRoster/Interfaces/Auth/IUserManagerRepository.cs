using Boostup.API.Entities;
using Boostup.API.Entities.Dtos.Request;
using Microsoft.AspNetCore.Identity;

namespace Boostup.API.Interfaces.Auth
{
    public interface IUserManagerRepository
    {
        Task<User?> GetUserByUserName(string userName);
        Task<User?> RegisterUser(OnboardRequest request);
        //Task<User?> GetUserByUserName(string userName);
        Task<Boolean> CheckPassword(User user, string password);
        Task<List<string>?> GetRoles(User user);
        Task<User?> GetUserById(string id);
        Task<string> GetPasswordResetLink(User user);
        Task<IdentityResult> UpdatePassword(UpdatePasswordReqest request);
    }
}
