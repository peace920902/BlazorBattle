using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Client.Service
{
    public class BattleService : IBattleService
    {
        private readonly HttpClient _http;

        public BattleService(HttpClient http)
        {
            _http = http;
        }

        public IList<BattleHistoryEntry> History { get; private set; } = new List<BattleHistoryEntry>();
        public BattleResult LastBattleResult { get; private set; } = new BattleResult();

        public async Task<BattleResult> StartBattle(int opponentId)
        {
            var result = await _http.PostAsJsonAsync("api/Battle", opponentId);
            LastBattleResult = await result.Content.ReadFromJsonAsync<BattleResult>();
            return LastBattleResult;
        }

        public async Task GetHistory()
        {
            History = await _http.GetFromJsonAsync<BattleHistoryEntry[]>("api/battle/history");
        }
    }
}