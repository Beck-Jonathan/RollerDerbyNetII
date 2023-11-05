using System.Data.SqlClient;

namespace DataAccessLayer
{
    internal static class SqlConnectionProvider
    {
        //use a factory method to ensure that there is only one method
        // in the entire project that has the database connection string

        static string connectionString = "Data Source=localhost;Initial Catalog=WFTDA;Integrated Security=True";

        public static SqlConnection GetConnection()
        {

            var connection = new SqlConnection(connectionString);
            return connection;
        }
    }
}
