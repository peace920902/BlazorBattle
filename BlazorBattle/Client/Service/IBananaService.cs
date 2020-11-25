using System;
using System.Threading.Tasks;

namespace BlazorBattle.Client.Service
{
    public interface IBananaService
    {
        event Action OnChange;
        public int Bananas { get; set; }
        void EatBananas(int amount);
        Task AddBananas(int amount);

        Task GetBananas();
    }
}