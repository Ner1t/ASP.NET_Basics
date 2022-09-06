using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IRewardBL
    {
        IRewardsDAO RewardsDAO { get; set; }
        void Add(Reward reward);
        void Delete(Reward reward);
        List<Reward> GetData();
        void Update(Reward reward);
        Reward Get(int id);
    }
}
