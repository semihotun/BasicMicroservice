using IdentityService.Models;
using IdentityService.Result;
using System.Threading.Tasks;

namespace IdentityService.Services
{
    public interface IAdminAuthService
    {
        Task<IDataResult<AccessToken>> Register(UserForRegisterDto userForRegisterDto);
        Task<IDataResult<AccessToken>> Login(UserForLoginDto userForLoginDto);
    }
}
