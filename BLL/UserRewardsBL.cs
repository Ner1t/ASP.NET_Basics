using Entities;
using Interfaces;
using System;
using System.Collections.Generic;


namespace BLL
{
    public class UserRewardsBL : IUserBL
    {
        public IUsersDAO UsersDAO { get; set; }

        public UserRewardsBL(IUsersDAO usersDAO)
        {
            UsersDAO = usersDAO;
        }

        public void Add(User user)
        {
            if (user == null)
            {
                throw new ArgumentException("user is null");
            }

            UsersDAO.Add(user);
        }

        public void Delete(User user)
        {
            UsersDAO.Delete(user);
        }

        public void Delete(int id)
        {
            UsersDAO.Delete(id);
        }

        public List<User> GetData()
        {
            return UsersDAO.GetAllUsers();
        }

        public void GiveReward(User user, Reward reward)
        {
            UsersDAO.GiveReward(user, reward);
        }

        public void TakeReward(User user, Reward reward)
        {
            UsersDAO.TakeReward(user, reward);
        }

        public void Update(User user)
        {
            UsersDAO.Update(user);
        }

        public User Get(int id)
        {
            return UsersDAO.Get(id);
        }
    }
}
