using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Client.Service
{
    public class LeaderBoardService:ILeaderBoardService
    {
        private readonly HttpClient _http;

        public LeaderBoardService(HttpClient http)
        {
            _http = http;
        }
        public IList<UserStatistic> LeaderBoard { get; set; }
        public async Task GetLeaderBoard()
        {
            LeaderBoard = await _http.GetFromJsonAsync<IList<UserStatistic>>("api/user/leaderboard");
        }
    }
}