using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorBattle.Shared;
using Blazored.Toast.Services;

namespace BlazorBattle.Client.Service
{
    public class UnitService : IUnitService
    {
        private readonly IToastService _toastService;
        private readonly HttpClient _http;
        private readonly IBananaService _bananaService;

        public UnitService(IToastService toastService, HttpClient http, IBananaService bananaService)
        {
            _toastService = toastService;
            _http = http;
            _bananaService = bananaService;
        }

        public IList<Unit> Units { get; set; } = new List<Unit>();
        public IList<UserUnit> MyUnits { get; set; } = new List<UserUnit>()
        {
            new UserUnit(){UnitId = 1,HitPoints = 100}
        };
        public async Task AddUnit(int unitId)
        {
            var unit = Units.FirstOrDefault(x => x.Id == unitId);
            if (unit == null) return;
            var res = await _http.PostAsJsonAsync<int>("api/UserUnit", unitId);

            if (res.StatusCode != HttpStatusCode.OK)
            {
                _toastService.ShowError(await res.Content.ReadAsStringAsync());
            }
            else
            {
                await _bananaService.GetBananas();
                _toastService.ShowSuccess($"Your {unit.Title} has been built!", "Unit built!");
            }
        }

        public async Task LoadUnitsAsync()
        {
            if (Units.Count == 0)
            {
                Units = await _http.GetFromJsonAsync<IList<Unit>>("api/unit");
            }
        }

        public async Task LoadUserUnitsAsync()
        {
            MyUnits = await _http.GetFromJsonAsync<IList<UserUnit>>("api/userunit");
        }

        public async Task ReviveArmy()
        {
            var result =await _http.PostAsJsonAsync<string>("api/userunit/revive", null);
            if(result.StatusCode == HttpStatusCode.OK)
                _toastService.ShowSuccess(await result.Content.ReadAsStringAsync());
            else
            
                _toastService.ShowError(await result.Content.ReadAsStringAsync());
            await LoadUserUnitsAsync();
            await _bananaService.GetBananas();
        }
    }
}