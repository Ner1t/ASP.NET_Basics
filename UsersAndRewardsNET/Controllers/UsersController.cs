using BLL;
using DAL;
using Entities;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace UsersAndRewardsNET.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUserBL _userBl;
        private readonly IRewardBL _rewardsBl;

        public UsersController()
        {
            #region "To connection to the database"
            //var usersDaodb = new UsersDAODB(ConnectionDB.Connect.GetConnectionString());
            //var rewardDaodb = new RewardsDAODB(ConnectionDB.Connect.GetConnectionString());
            #endregion

            #region "To connection to the moq"
            var usersDaodb = new UsersDAOList();
            var rewardDaodb = new RewardsDAOList();
            #endregion

            _userBl = new UserRewardsBL(usersDaodb);
            _rewardsBl = new RewardsBL(rewardDaodb);
        }

        public IActionResult PageUsers()
        {
            var users = _userBl.GetData();
            return View(users);
        }

        public IActionResult Delete(int id)
        {
            var user = _userBl.Get(id);

            _userBl.Delete(user);
            return RedirectToAction("PageUsers");
        }

        public IActionResult UserEdit(int id)
        {
            var currentUser = _userBl.Get(id);
            return View(UserView.GetViewModel(currentUser, _rewardsBl.GetData()));
        }

        [HttpPost]
        public IActionResult UserEdit(UserView user)
        {
            var userToSave = new User()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,

                OwnRewards = user.AvailableRewards
                    .Where(x => x.Checked)
                    .Select(x => new Reward()
                    {
                        Id = x.Id,
                        Description = x.Description,
                        Title = x.Title
                    }).ToList()
            };

            _userBl.Update(userToSave);

            return RedirectToAction("PageUsers");
        }
        public IActionResult CreateUser()
        {
            return View("CreateUser");
        }

        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            var userToSave = new User()
            {
                BirthDate = user.BirthDate,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _userBl.Add(userToSave);

            return RedirectToAction("PageUsers");
        }
    }
}