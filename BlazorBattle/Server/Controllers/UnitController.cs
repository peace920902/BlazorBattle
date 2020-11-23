using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorBattle.Server.data;
using BlazorBattle.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorBattle.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitController : Controller
    {
        private readonly DataContext _dataContext;

        public UnitController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        [HttpGet] 
        public async Task<OkObjectResult> GetUnit()
        {
            var units =await _dataContext.Units.ToListAsync();
            return Ok(units);
        }

        [HttpPost]
        public async Task<IActionResult> AddUnit(Unit unit)
        {
            await _dataContext.Units.AddAsync(unit);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Units.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUnit(int id,Unit updateUnit)
        {
            var unit = await _dataContext.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound($"unit not found id:{id}");
            unit.Title = updateUnit.Title;
            unit.BananaCost = updateUnit.BananaCost;
            unit.HitPoints = updateUnit.HitPoints;
            unit.Defense = updateUnit.Defense;
            unit.Attack = updateUnit.Attack;
            await _dataContext.SaveChangesAsync();
            return Ok(updateUnit );
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUnit(int id)
        {
            var unit = await _dataContext.Units.FirstOrDefaultAsync(u => u.Id == id);
            if (unit == null) return NotFound($"unit not found id:{id}");

            _dataContext.Units.Remove(unit);
            await _dataContext.SaveChangesAsync();
            return Ok(await _dataContext.Units.ToListAsync());
        }
    }
}