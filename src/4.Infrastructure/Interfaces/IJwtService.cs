using Domain.Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(User user);

        string GenerateRefreshToken();
    }
}
