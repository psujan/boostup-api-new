using Boostup.API.Entities;

namespace Boostup.API.Interfaces.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(User user, List<string> roles);
    }
}
