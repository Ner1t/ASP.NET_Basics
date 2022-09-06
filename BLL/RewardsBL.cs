using Entities;
using Interfaces;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class RewardsBL : IRewardBL
    {
        public IRewardsDAO RewardsDAO { get; set; }

        public RewardsBL(IRewardsDAO rewardsDAO)
        {
            RewardsDAO = rewardsDAO;
        }

        public Reward Get(int id)
        {
            return RewardsDAO.Get(id);
        }

        public void Add(Reward reward)
        {
            if (reward == null)
            {
                throw new ArgumentException("reward is null");
            }

            RewardsDAO.Add(reward);
        }

        public void Delete(Reward reward)
        {
            RewardsDAO.Delete(reward);
        }

        public List<Reward> GetData()
        {
            return RewardsDAO.GetAllRewards();
        }

        public void Update(Reward reward)
        {
            RewardsDAO.Update(reward);
        }
    }
}
