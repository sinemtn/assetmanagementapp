using Npgsql;
namespace Auth;

public class Authentication
{
    private string _connString;

    public Authentication(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<bool> IsAuthenticated(string username, string password)
    {
        string sql = "SELECT id, username, role FROM user WHERE username = @username and password = @password";

        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);

        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@username", username);
            cmd.Parameters.AddWithValue("@password", password);

            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows) return false;
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Database error", ex);
        }
    }
}
