using System.Linq;
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
    public class UserUnitController : Controller
    {
        private readonly DataContext _dataContext;
        private readonly IUtilityService _utilityService;

        public UserUnitController(DataContext dataContext, IUtilityService utilityService)
        {
            _dataContext = dataContext;
            _utilityService = utilityService;
        }

        [HttpPost]
        public async Task<IActionResult> BuildUserUnit([FromBody] int unitId)
        {
            var unit = await _dataContext.Units.FirstOrDefaultAsync<Unit>(u => u.Id == unitId);
            var user = await _utilityService.GetUser();
            if (user.Bananas < unit.BananaCost)
            {
                return BadRequest("Not enough bananas!");
            }

            user.Bananas -= unit.BananaCost;

            var userUnit = new UserUnit()
            {
                UnitId = unit.Id,
                UserId = user.Id,
                HitPoints = unit.HitPoints
            };

            await _dataContext.UserUnits.AddAsync(userUnit);
            await _dataContext.SaveChangesAsync();
            return Ok(userUnit);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserUnit()
        {
            var user = await _utilityService.GetUser();
            var userUnit = await _dataContext.UserUnits.Where(x => x.UserId == user.Id).ToListAsync();

            var response = userUnit.Select(u => new UserUnitResponse
            {
                UnitId = u.UnitId,
                HitPoints = u.HitPoints
            });

            return Ok(response);
        }
    }
}