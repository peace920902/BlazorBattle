using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Server.data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<bool> UserExist(string email);
    }
}