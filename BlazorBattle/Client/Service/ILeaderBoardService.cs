using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlazorBattle.Shared;

namespace BlazorBattle.Client.Service
{
    public interface ILeaderBoardService
    {
        IList<UserStatistic> LeaderBoard { get; set; }
        Task GetLeaderBoard();
    }
}