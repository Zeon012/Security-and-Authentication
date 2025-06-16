using System;
using System.Data;
using System.Data.SqlClient;

public class DatabaseHelper
{
    private readonly string _connectionString;

    public DatabaseHelper(string connectionString)
    {
        _connectionString = connectionString;
    }

    // Securely retrieves user information by username using parameterized queries
    public DataTable GetUserByUsername(string username)
    {
        using (SqlConnection conn = new SqlConnection(_connectionString))
        using (SqlCommand cmd = new SqlCommand("SELECT UserID, Username, Email FROM Users WHERE Username = @Username", conn))
        {
            cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.VarChar, 100) { Value = username });
            conn.Open();
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable result = new DataTable();
                adapter.Fill(result);
                return result;
            }
        }
    }
}
