using System.Net.Http;
using System.Threading.Tasks;
using DocHelper.Domain.Auth;

namespace DocHelper.Domain.Common.Services
{
    public interface IAuthService
    {
        Task<LoginResult> Login(LoginModel loginModel);
        Task Logout();
        Task<RegisterResult> Register(RegisterModel registerModel);
    }
}