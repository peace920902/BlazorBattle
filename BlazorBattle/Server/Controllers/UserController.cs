using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorBattle.Server.data;
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

        // GET
        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        private async Task<User> GetUser()=> await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == GetUserId());


        [HttpGet("GetBananas")]
        public async Task<IActionResult> GetBananas()
        {
            var user = await GetUser();
            return Ok(user.Bananas);
        }

        [HttpPut("AddBananas")]
        public async Task<IActionResult> AddBananas([FromBody] int bananas)
        {
            var user = await GetUser();
            user.Bananas += bananas;

            await _dataContext.SaveChangesAsync();
            return Ok(user.Bananas);
        }
    }
}