using Entities;
using System.Collections.Generic;

namespace Interfaces
{
    public interface IUserBL
    {
        IUsersDAO UsersDAO { get; set; }
        List<User> GetData();
        User Get(int id);
        void Update(User user);
        void Add(User user);
        void Delete(User user);
        void GiveReward(User user, Reward reward);
        void TakeReward(User user, Reward reward);
    }
}
