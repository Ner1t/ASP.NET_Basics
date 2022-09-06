using Data;
using Entities;
using Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DAL
{
    public class UsersDAOList : IUsersDAO
    {
        private List<User> _users = new List<User>();
        readonly ListData _data = new ListData();

        public List<User> GetAllUsers()
        {
            foreach (var item in _data.Users)
            {
                _users.Add(item);
            }
            return _users;
        }

        public void Delete(User user)
        {
            _users.Remove(user);
        }

        public void Add(User user)
        {
            _users.Add(user);
        }

        public User Get(int id)
        {
            return _data.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Update(User user)
        {
            var userToUpdate = Get(user.Id);
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.BirthDate = user.BirthDate;
        }

        public void TakeReward(User user, Reward reward)
        {
            user.TakeReward(reward);
        }

        public void GiveReward(User user, Reward reward)
        {
            user.GiveReward(reward);
        }

        public void Delete(int id)
        {
            _users = _users.Where(x => x.Id != id).ToList();
        }
    }
}

