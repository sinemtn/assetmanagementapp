using Npgsql;

namespace User;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task CreateUser(Model model)
    {
        string sql = @"
            INSERT INTO users (user_id, name, email, password, role, active) 
            VALUES (@userId, @name, @email, @password, @role, @active)
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database bermasalah: {ex.Message}");
        }
        cmd.Parameters.AddWithValue("@userId", model.UserId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@email", model.Email);
        cmd.Parameters.AddWithValue("@password", model.Password);
        cmd.Parameters.AddWithValue("@role", model.Role);
        cmd.Parameters.AddWithValue("@active", model.Active);

        var count = 0;
        try
        {
            count = await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (count == 0)
        {
            throw new Exception("Tidak ada user yang ditambahkan");
        }
        
    }

    public async Task UpdateUser(string userId, Model model)
    {
        string sql = @"
            UPDATE users
            SET name = @name, email = @email, password = @password, role = @role, active = @active
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
            throw new Exception($"Database bermasalah: {ex.Message}");
        }

        cmd.Parameters.AddWithValue("@userid", userId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@email", model.Email);
        cmd.Parameters.AddWithValue("@password", model.Password);
        cmd.Parameters.AddWithValue("@role", model.Role);
        cmd.Parameters.AddWithValue("@active", model.Active);
        
        var count = 0;
        try
        {
            count = await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (count == 0)
        {
            throw new Exception("Tidak ada user yang diperbarui");
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
            throw new Exception($"Database bermasalah: {ex.Message}");
        }

        cmd.Parameters.AddWithValue("@userid", userId);
        
        var count = 0;
        try
        {
            count = await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        if (count == 0)
        {
            throw new Exception("User tidak ditemukan");
        }
        

    }

    public async Task<List<Model>> GetUsers()
    {
        List<Model> users = new();
        string sql = "SELECT user_id, user_name, email, password, role, active FROM users ORDER BY user_id";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database bermasalah: {ex.Message}");
        }

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            Model model = new()
            {
                UserId = reader.GetString(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Password = reader.GetString(3),
                Role = reader.GetInt32(4),
                Active = reader.GetBoolean(5)
            };
            users.Add(model);
        }
        
        return users;
    }

    public async Task<Model?> GetUserById(int userId)
    {
        Model? model = null;
        string sql = "SELECT user_id, user_name, email, password, role, active FROM users WHERE user_id = @userid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database bermasalah: {ex.Message}");
        }

        cmd.Parameters.AddWithValue("@userid", userId);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            model = new()
            {
                UserId = reader.GetString(0),
                Name = reader.GetString(1),
                Email = reader.GetString(2),
                Password = reader.GetString(3),
                Role = reader.GetInt32(4),
                Active = reader.GetBoolean(5)
            };
        }
        
        
        return model;
    }
}