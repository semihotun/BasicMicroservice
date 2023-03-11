using IdentityService.Context.ContextTable;
using IdentityService.Models;

namespace IdentityService.Services
{
    public interface IJwtService
    {
        AccessToken CreateToken(AdminUser user);
    }
}
