using EnglishWordHelperClient.Models;
using System.Threading.Tasks;

namespace EnglishWordHelperClient.HttpServices.Interfaces
{
    public interface IAuthHttpService
    {
        Task<bool> Login(UserLogin loginModel);
        Task Logout();
        Task<string> RefreshToken();
    }
}
