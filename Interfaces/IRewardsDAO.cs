using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IRewardsDAO
    {
        List<Reward> GetAllRewards();
        void Update(Reward reward);
        void Delete(Reward reward);
        void Add(Reward reward);
        Reward Get(int id);
    }
}
