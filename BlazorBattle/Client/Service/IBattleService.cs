using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Client.Service
{
    public interface IBattleService
    {
        IList<BattleHistoryEntry> History { get; }
        public BattleResult LastBattleResult { get; }
        Task<BattleResult> StartBattle(int opponentId);
        Task GetHistory();
    }
}