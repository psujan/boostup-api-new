using Boostup.API.Entities;

namespace Boostup.API.Interfaces.Auth
{
    public interface IUserManagerRepository
    {
        Task<User?> GetUserByUserName(string userName);
        //Task<User?> RegisterUser(RegisterRequestDto request);
        //Task<User?> GetUserByUserName(string userName);
        Task<Boolean> CheckPassword(User user, string password);
        Task<List<string>?> GetRoles(User user);

        Task<User?> GetUserById(string id);
    }
}
