using EasyLibrary.Core.Models;

namespace EasyLibrary.Core.Abstractions.Auth
{
    public interface IJwtProvider
    {
        string GenerateToken(User user);
    }
}
