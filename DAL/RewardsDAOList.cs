using Data;
using Entities;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class RewardsDAOList : IRewardsDAO
    {
        public List<Reward> _rewards = new List<Reward>();
        readonly ListData _data = new ListData();

        public List<Reward> GetAllRewards()
        {
            foreach (var item in _data.Rewards)
            {
                _rewards.Add(item);
            }

            return _rewards;
        }

        public void Delete(Reward reward)
        {
            //foreach (User user in ListOfUsers.Where((x => x.OwnRewards.Contains(reward))))
            //{
            //    _userEditorBL.TakeReward(user, reward);
            //}

            _rewards.Remove(reward);
        }
        public void Add(Reward reward)
        {
            _rewards.Add(reward);
        }

        public void Update(Reward reward)
        {
            var rewardToUpdate = Get(reward.Id);
            rewardToUpdate.Title = reward.Title;
            rewardToUpdate.Description = reward.Description;
        }

        public Reward Get(int id)
        {
            return _data.Rewards.FirstOrDefault(x => x.Id == id);
        }
    }
}
