using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class RewardsDAODB : IRewardsDAO
    {
        private readonly string _connectionString;

        public RewardsDAODB(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Reward> GetAllRewards()
        {
            var Rewards = new List<Reward>();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();

                command.CommandText = "SELECT Id, Title, Description FROM dbo.Rewards";
                connection.Open();

                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var reward = new Reward();
                    reward.Id = reader.GetInt32(0);
                    reward.Title = reader.GetString(1);
                    reward.Description = reader.GetString(2);
                    Rewards.Add(reward);
                }
            }
            return Rewards;
        }

        public void Delete(Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "DeleteReward";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("Id", SqlDbType.Int).Value = reward.Id;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public void Add(Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                // command.CommandText = $"INSERT INTO  dbo.Users VALUES ('{user.FirstName}','{user.LastName}','{user.BirthDate}')";
                command.CommandText += " SELECT SCOPE_IDENTITY()";
                command.CommandText = "AddReward";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("Title", SqlDbType.NVarChar).Value = reward.Title;
                command.Parameters.Add("Description", SqlDbType.NVarChar).Value = reward.Description;

                connection.Open();
                var id = Convert.ToInt32(command.ExecuteScalar());
                reward.Id = id;
            }
        }

        public void Update(Reward reward)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "UpdateReward";
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("Id", SqlDbType.Int).Value = reward.Id;
                command.Parameters.Add("Title", SqlDbType.NVarChar).Value = reward.Title;
                command.Parameters.Add("Description", SqlDbType.NVarChar).Value = reward.Description;

                connection.Open();
                command.ExecuteNonQuery();
            }
        }

        public Reward Get(int id)
        {
            var Reward = new Reward();
            using (var connection = new SqlConnection(_connectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM Rewards WHERE Id = '{id}'";
                // command.Parameters.Add("id", SqlDbType.Int).Value = id;
                connection.Open();

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var reward = new Reward();
                        reward.Id = reader.GetInt32(0);
                        reward.Title = reader.GetString(1);
                        reward.Description = reader.GetString(2);

                        Reward = reward;
                    }
                }
            }
            return Reward;
        }
    }
}
