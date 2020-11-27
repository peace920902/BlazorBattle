using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattle.Server.data;
using BlazorBattle.Server.Services;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUtilityService _utilityService;

        // GET
        public UserController(DataContext dataContext,IUtilityService utilityService)
        {
            _dataContext = dataContext;
            _utilityService = utilityService;
        }

        [HttpGet("GetBananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await _utilityService.GetUser();
            return Ok(user.Bananas);
        }

        [HttpPut("AddBananas")]
        public async Task<IActionResult> AddBananas([FromBody] int bananas)
        {
            var user = await _utilityService.GetUser();
            user.Bananas += bananas;

            await _dataContext.SaveChangesAsync();
            return Ok(user.Bananas);
        }

        [HttpGet("LeaderBoard")]
        public async Task<IActionResult> GetLeaderBoard()
        {
            var users = await _dataContext.Users.Where(user=>!user.IsDeleted).ToListAsync();
            users = users.OrderByDescending(x => x.Victories).ThenBy(u => u.Defeats).ThenBy(x => x.DateCreated).ToList();
            var rank = 1;
            var response = users.Select(u => new UserStatistic
            {
                Rank = rank++,
                UserId = u.Id,
                UserName = u.UserName,
                Battles = u.Battles,
                Victories = u.Victories,
                Defeats = u.Defeats
            });
            return Ok(response);
        }
    }
}