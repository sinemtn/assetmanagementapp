using Npgsql;

namespace User;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<int> CreateUser(Model model)
    {
        string sql = @"
            INSERT INTO users (user_name, email, password, role) 
            VALUES (@name, @email, @password, @role)
            RETURNING user_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@name", model.Name);
            cmd.Parameters.AddWithValue("@email", model.Email);
            cmd.Parameters.AddWithValue("@password", model.Password);
            cmd.Parameters.AddWithValue("@role", model.Role);
            var result = await cmd.ExecuteScalarAsync();
            if (result == null) throw new Exception("Failed to retrieve inserted user ID");
            var id = (int)result;

            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateUser(int userId, Model model)
    {
        string sql = @"
            UPDATE users
            SET user_name = @name, email = @email, password = @password, role = @role
            WHERE user_id = @userid
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);

        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }

        cmd.Parameters.AddWithValue("@userid", userId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@email", model.Email);
        cmd.Parameters.AddWithValue("@password", model.Password);
        cmd.Parameters.AddWithValue("@role", model.Role);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No user found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteUser(int userId)
    {
        string sql = "DELETE FROM users WHERE user_id = @userid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }

        cmd.Parameters.AddWithValue("@userid", userId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No user found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetUsers()
    {
        List<Model> users = new();
        string sql = "SELECT user_id, user_name, email, password, role FROM users ORDER BY user_id";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Model model = new()
                {
                    UserId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
                users.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return users;
    }

    public async Task<Model?> GetUserById(int userId)
    {
        Model? model = null;
        string sql = "SELECT user_id, user_name, email, password, role FROM users WHERE user_id = @userid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@userid", userId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    UserId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Email = reader.GetString(2),
                    Password = reader.GetString(3),
                    Role = reader.GetString(4)
                };
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return model;
    }
}