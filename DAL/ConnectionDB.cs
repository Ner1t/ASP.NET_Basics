using System.Data.SqlClient;

namespace DAL
{
    public class ConnectionDB
    {
        public static class Connect
        {
            public static string GetConnectionString()
            {
                var connectionStringBuilder = new SqlConnectionStringBuilder();
                connectionStringBuilder.DataSource = @"HOME-PC\SQLEXPRESS";
                connectionStringBuilder.InitialCatalog = "UsersAndRewards";
                connectionStringBuilder.IntegratedSecurity = true;
                var connection = connectionStringBuilder.ToString();
                return connection;
            }
        }
    }
}
