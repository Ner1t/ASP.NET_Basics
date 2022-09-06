using System.Collections.Generic;
using System.Linq;

namespace Entities
{
    public class RewardView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Checked { get; set; }

        public static RewardView GetView(Reward reward)
        {
            var model = new RewardView
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description
            };

            return model;
        }

        public static RewardView GetViewModel(Reward reward, List<Reward> userRewards)
        {
            var model = new RewardView
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description,
                Checked = userRewards.Any(r => r.Id == reward.Id)
            };
            return model;
        }
    }
}
