using Npgsql;

namespace Sparepart;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task CreateSparepart(Model model)
    {
        string sql = @"
            INSERT INTO ms_sparepart (sparepart_id, name, active) 
            VALUES (@sparepartid, @name, @active)
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@sparepartid", model.SparepartID);
            cmd.Parameters.AddWithValue("@name", model.Name);
            cmd.Parameters.AddWithValue("@active", model.Active);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateSparepart(string sparepartId, Model model)
    {
        string sql = @"
            UPDATE ms_sparepart
            SET name = @name, active = @active
            WHERE sparepart_id = @sparepartid
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

        cmd.Parameters.AddWithValue("@sparepartid", sparepartId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@active", model.Active);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No sparepart found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteSparepart(string sparepartId)
    {
        string sql = "DELETE FROM ms_sparepart WHERE sparepart_id = @sparepartid";
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

        cmd.Parameters.AddWithValue("@sparepartid", sparepartId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No sparepart found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetSpareparts()
    {
        List<Model> spareparts = new();
        string sql = "SELECT sparepart_id, name, active FROM ms_sparepart ORDER BY sparepart_id";
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
                    SparepartID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Active = reader.GetBoolean(2)
                };
                spareparts.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return spareparts;
    }

    public async Task<Model?> GetSparepartById(string sparepartId)
    {
        Model? model = null;
        string sql = "SELECT sparepart_id, name, active FROM ms_sparepart WHERE sparepart_id = @sparepartid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@sparepartid", sparepartId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    SparepartID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Active = reader.GetBoolean(2)
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