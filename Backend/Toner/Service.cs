using Npgsql;

namespace Toner;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task CreateToner(Model model)
    {
        string sql = @"
            INSERT INTO ms_toner (toner_id, name, category, active) 
            VALUES (@tonerid, @name, @category, @active)
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@tonerid", model.TonerId);
            cmd.Parameters.AddWithValue("@name", model.Name);
            cmd.Parameters.AddWithValue("@category", model.Category);
            cmd.Parameters.AddWithValue("@active", model.Active);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateToner(string tonerId, Model model)
    {
        string sql = @"
            UPDATE ms_toner
            SET name = @name, category = @category, active = @active
            WHERE toner_id = @tonerid
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

        cmd.Parameters.AddWithValue("@tonerid", tonerId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@category", model.Category);
        cmd.Parameters.AddWithValue("@active", model.Active);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No toner found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteToner(string tonerId)
    {
        string sql = "DELETE FROM ms_toner WHERE toner_id = @tonerid";
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

        cmd.Parameters.AddWithValue("@tonerid", tonerId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No toner found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetToners()
    {
        List<Model> toners = new();
        string sql = "SELECT toner_id, name, category, active FROM ms_toner ORDER BY toner_id";
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
                    TonerId = reader.GetString(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Active = reader.GetBoolean(3)
                };
                toners.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return toners;
    }

    public async Task<Model?> GetTonerById(string tonerId)
    {
        Model? model = null;
        string sql = "SELECT toner_id, name, category, active FROM ms_toner WHERE toner_id = @tonerid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@tonerid", tonerId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    TonerId = reader.GetString(0),
                    Name = reader.GetString(1),
                    Category = reader.GetString(2),
                    Active = reader.GetBoolean(3)
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