using System;
using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class UserView
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Reward> Rewards { get; set; }
        public List<RewardView> AvailableRewards { get; set; }

        public User ToUser()
        {
            return new User
            {
                Id = Id,
                FirstName = FirstName,
                LastName = LastName,
                BirthDate = BirthDate,
                OwnRewards = AvailableRewards
                    .Where(r => r.Checked == true)
                    .Select(r => new Reward
                    {
                        Id = r.Id,
                        Title = r.Title,
                        Description = r.Description
                    }).ToList()
            };
        }
        public static UserView GetViewModel(User user, List<Reward> availableRewards)
        {
            var userModel = new UserView
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Rewards = user.OwnRewards
            };
            var rewards = new List<RewardView>();
            foreach (var reward in availableRewards)
            {
                rewards.Add(RewardView.GetViewModel(reward, user.OwnRewards));
            }

            userModel.AvailableRewards = rewards.ToList();
            return userModel;
        }
    }
}
