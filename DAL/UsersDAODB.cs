using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DAL
{
    public class UsersDAODB : IUsersDAO
    {
        private readonly string _connectionString;

        public UsersDAODB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<User> GetAllUsers()
        {
            var Users = new List<User>();

            using (var connection = new SqlConnection(_connectionString))
            {
                var commandTakefromUsers = connection.CreateCommand();

                commandTakefromUsers.CommandText = "SELECT Id, FirstName, LastName, DateOfBirth FROM dbo.Users";

                connection.Open();

                using (var firstReader = commandTakefromUsers.ExecuteReader())
                {
                    while (firstReader.Read())
                    {
                        var user = new User
                        {
                            Id = firstReader.GetInt32(0),
                            FirstName = firstReader.GetString(1),
                            LastName = firstReader.GetString(2),
                            BirthDate = firstReader.GetDateTime(3)
                        };

                        Users.Add(user);
                    }
                }
                foreach (User user in Users)
                {
                    var commandTakeFormUserRewards = connection.CreateCommand();
                    commandTakeFormUserRewards.CommandText = $"SELECT R.Id , R.Title , R.Description FROM UserRewards UR LEFT JOIN Rewards R on UR.RewardId = R.Id WHERE UR.UserId = '{ user.Id}'";

                    using (var secondReader = commandTakeFormUserRewards.ExecuteReader())

                    {
                        while (secondReader.Read())
                        {
                            var reward = new Reward
                            {
                                Id = secondReader.GetInt32(0),
                                Title = secondReader.GetString(1),
                                Description = secondReader.GetString(2)
                            };

                            user.OwnRewards.Add(reward);
                        }
                    }
                }
                return Users;
            }
        }

        public void Delete(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "DeleteUser";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("Id", SqlDbType.Int).Value = user.Id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"DELETE FROM Users WHERE Id = '{id}'";
                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Add(User user)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    //command.CommandText = $"INSERT INTO  dbo.Users VALUES ('{user.FirstName}','{user.LastName}','{user.BirthDate}')";
                    command.CommandText += " SELECT SCOPE_IDENTITY()";
                    command.CommandText = "AddUser";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("FirstName", SqlDbType.NVarChar).Value = user.FirstName;
                    command.Parameters.Add("LastName", SqlDbType.NVarChar).Value = user.LastName;
                    command.Parameters.Add("DateOfBirth", SqlDbType.Date).Value = user.BirthDate;

                    connection.Open();

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    user.Id = id;
                }
            }
        }
        public void Update(User user)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();
            var currentRewardsId = new List<int>();

            var userRewardsCommand = connection.CreateCommand();
            userRewardsCommand.CommandText = $"SELECT RewardId FROM UserRewards WHERE UserId = {user.Id}";
            var reader = userRewardsCommand.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var rewardId = (int)reader.GetValue(0);
                    currentRewardsId.Add(rewardId);
                }
            }

            reader.Close();

            foreach (var currentReward in currentRewardsId)
            {
                if (user.OwnRewards.All(x => x.Id != currentReward))
                {
                    var deleteRewardFromUser = connection.CreateCommand();
                    deleteRewardFromUser.CommandText = $"DELETE FROM UserRewards WHERE UserId = {user.Id} AND RewardId = {currentReward}";
                    deleteRewardFromUser.ExecuteNonQuery();
                }
            }

            foreach (var newReward in user.OwnRewards)
            {
                if (!currentRewardsId.Contains(newReward.Id))
                {
                    var insertRewardToUser = connection.CreateCommand();
                    insertRewardToUser.CommandText = $"INSERT INTO UserRewards (UserId, RewardId) VALUES ({user.Id}, {newReward.Id})";
                    insertRewardToUser.ExecuteNonQuery();
                }
            }

            var command = connection.CreateCommand();
            command.CommandText = "UpdateUser";
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("Id", SqlDbType.Int).Value = user.Id;
            command.Parameters.Add("FirstName", SqlDbType.NVarChar).Value = user.FirstName;
            command.Parameters.Add("LastName", SqlDbType.NVarChar).Value = user.LastName;
            command.Parameters.Add("DateofBirth", SqlDbType.Date).Value = user.BirthDate;
            command.ExecuteNonQuery();
        }

        public void GiveReward(User user, Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = $"INSERT INTO  dbo.UserRewards VALUES ('{user.Id}','{reward.Id}')";

                //command.Prepare();
                //command.Parameters[0].Value = user.Id;
                //command.Parameters[1].Value = reward.Id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public int CheckContainReward(User user, Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "CheckContainReward";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("RewardId", SqlDbType.Int).Value = reward.Id;
                connection.Open();

                int answer = (int)command.ExecuteScalar();
                return answer;
            }
        }
        public void TakeReward(User user, Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "TakeReward";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("UserId", SqlDbType.Int).Value = user.Id;
                command.Parameters.Add("RewardId", SqlDbType.Int).Value = reward.Id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public User Get(int id)
        {
            var User = new User();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "Get";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("id", SqlDbType.Int).Value = id;
                connection.Open();

                using (var firstReader = command.ExecuteReader())
                {
                    while (firstReader.Read())
                    {
                        var user = new User
                        {
                            Id = firstReader.GetInt32(0),
                            FirstName = firstReader.GetString(1),
                            LastName = firstReader.GetString(2),
                            BirthDate = firstReader.GetDateTime(3)
                        };

                        User = user;
                    }
                }
                var command1 = connection.CreateCommand();
                command1.CommandText = $"SELECT R.Id , R.Title , R.Description FROM UserRewards UR LEFT JOIN Rewards R on UR.RewardId = R.Id WHERE UR.UserId = '{User.Id}'";

                using (var secondReader = command1.ExecuteReader())
                {
                    while (secondReader.Read())
                    {
                        var reward = new Reward
                        {
                            Id = secondReader.GetInt32(0),
                            Title = secondReader.GetString(1),
                            Description = secondReader.GetString(2)
                        };

                        User.OwnRewards.Add(reward);
                    }
                }
            }
            return User;
        }
    }
}
