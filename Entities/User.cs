using System;
using System.Collections.Generic;
using System.Linq;



namespace Entities
{
    public class User
    {
        private static int _userId = 1;
        private DateTime _birthDay { get; set; }

        public User()
        {
            _userId++;
            Id = _userId;
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Reward> OwnRewards = new List<Reward>();

        public DateTime BirthDate
        {
            get
            {
                return _birthDay;
            }
            set
            {
                _birthDay = value;
            }
        }

        public override string ToString()
        {
            return FirstName;
        }

        public int Age
        {
            get
            {
                DateTime today = DateTime.Today.Date;
                int age = today.Year - BirthDate.Year;
                if (BirthDate.Date > today.AddYears(-age))
                {
                    age--;
                }
                return age;
            }
        }
        public string Rewards { get { return string.Concat(OwnRewards.Select(x => x.Title)); } }

        public void GiveReward(Reward reward)
        {
            OwnRewards.Add(reward);
        }

        public void TakeReward(Reward reward)
        {
            OwnRewards.Remove(reward);
        }
    }
}
