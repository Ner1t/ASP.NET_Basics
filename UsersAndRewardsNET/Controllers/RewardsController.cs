using BLL;
using DAL;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace UsersAndRewardsNET.Controllers
{
    public class RewardsController : Controller
    {
        private readonly IRewardBL _rewardsBl;

        public RewardsController()
        {
            #region "To connection to the database"
            //var rewardDaodb = new RewardsDAODB(ConnectionDB.Connect.GetConnectionString());
            #endregion

            #region "To connection to the moq"
            var rewardDaodb = new RewardsDAOList();
            #endregion

            _rewardsBl = new RewardsBL(rewardDaodb);

        }

        public IActionResult PageRewards()
        {
            var rewards = _rewardsBl.GetData();
            return View(rewards);
        }

        public IActionResult Delete(int id)
        {
            var reward = _rewardsBl.Get(id);
            _rewardsBl.Delete(reward);

            return RedirectToAction("PageRewards");
        }

        public IActionResult RewardEdit(int id)
        {
            var currentReward = _rewardsBl.Get(id);
            return View(currentReward/*RewardView.GetView(currentReward)*/);
        }

        [HttpPost]
        public IActionResult RewardEdit(Reward reward)
        {
            var rewardToSave = new Reward()
            {
                Id = reward.Id,
                Title = reward.Title,
                Description = reward.Description
            };

            _rewardsBl.Update(rewardToSave);
            return RedirectToAction("PageRewards");
        }

        public IActionResult CreateReward()
        {
            return View("CreateReward");
        }

        [HttpPost]
        public IActionResult CreateReward(Reward reward)
        {
            var rewardToSave = new Reward()
            {
                Title = reward.Title,
                Description = reward.Description,
            };
            _rewardsBl.Add(rewardToSave);

            return RedirectToAction("PageRewards");
        }
    }
}

