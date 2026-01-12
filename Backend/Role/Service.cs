using Npgsql;

namespace Role;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<string> CreateRole(Model model)
    {
        string sql = @"
            INSERT INTO role (role_id, name) 
            VALUES (@id, @name)
            RETURNING role_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", model.RoleId);
            cmd.Parameters.AddWithValue("@name", model.Name);
            var result = await cmd.ExecuteScalarAsync();
            if (result == null) throw new Exception("Failed to retrieve inserted user ID");
            var id = (string)result;

            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateRole(string roleId, Model model)
    {
        string sql = @"
            UPDATE role
            SET name = @name
            WHERE role_id = @id
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

        cmd.Parameters.AddWithValue("@id", roleId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No role found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteRole(string roleId)
    {
        string sql = "DELETE FROM role WHERE role_id = @id and not exists (select 1 from users where role = @id)";
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

        cmd.Parameters.AddWithValue("@id", roleId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No role found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetRoles()
    {
        List<Model> roles = new();
        string sql = "SELECT role_id, name FROM role ORDER BY role_id";
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
                    RoleId = reader.GetString(0),
                    Name = reader.GetString(1)
                };
                roles.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return roles;
    }

    public async Task<Model?> GetRoleById(int roleId)
    {
        Model? model = null;
        string sql = "SELECT role_id, name FROM role WHERE role_id = @id";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", roleId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    RoleId = reader.GetString(0),
                    Name = reader.GetString(1)
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