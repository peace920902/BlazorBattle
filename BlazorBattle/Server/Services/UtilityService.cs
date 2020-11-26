using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattle.Server.data;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.Services
{
    public class UtilityService:IUtilityService
    {
        private readonly DataContext _dataContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UtilityService(DataContext dataContext, IHttpContextAccessor httpContextAccessor)
        {
            _dataContext = dataContext;
            _httpContextAccessor = httpContextAccessor;
        }


        public async Task<User> GetUser()
        {
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = await _dataContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

    }
}