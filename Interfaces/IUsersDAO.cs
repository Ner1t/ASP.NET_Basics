using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IUsersDAO
    {
        List<User> GetAllUsers();
        void Delete(User user);
        void Delete(int id);
        void Add(User user);
        void Update(User user);
        User Get(int id);
        void TakeReward(User user, Reward reward);
        void GiveReward(User user, Reward reward);
    }
}
