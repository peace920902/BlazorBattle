using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Client.Service
{
    public interface IUnitService
    {
        IList<Unit> Units { get; set; }
        IList<UserUnit> MyUnits { get; set; }
        Task AddUnit(int unitId);

        Task LoadUnitsAsync();

        Task LoadUserUnitsAsync();
    }
}