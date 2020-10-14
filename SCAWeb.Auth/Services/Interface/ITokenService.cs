using SCAWeb.Auth.Models;

namespace SCAWeb.Auth.Services.Interface
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
