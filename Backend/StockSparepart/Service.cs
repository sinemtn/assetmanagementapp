using Npgsql;

namespace StockSparepart;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<Model> CreateStockSparepart(Model model)
    {
        string sql = @"
            INSERT INTO stock_sparepart (sparepart, location, branch, qty, customer, notes)
            SELECT @sparepart, @location, @branch, @qty, @customer, @notes
            WHERE EXISTS (SELECT 1 FROM ms_sparepart WHERE sparepart_id = @sparepart)
            RETURNING id, created_at
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

        cmd.Parameters.AddWithValue("@sparepart", model.Sparepart);
        cmd.Parameters.AddWithValue("@location", (object?)model.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@branch", (object?)model.Branch ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@qty", model.Qty);
        cmd.Parameters.AddWithValue("@customer", (object?)model.Customer ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@notes", (object?)model.Notes ?? DBNull.Value);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            model.Id = reader.GetInt64(0);
            model.CreatedAt = reader.GetDateTime(1);
            return model;
        }

        throw new KeyNotFoundException("Sparepart does not exist in master data.");
    }

    public async Task UpdateStockSparepart(long id, Model model)
    {
        string sql = @"
            UPDATE stock_sparepart
            SET sparepart = @sparepart, location = @location, branch = @branch,
                qty = @qty, customer = @customer, notes = @notes
            WHERE id = @id
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

        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@sparepart", model.Sparepart);
        cmd.Parameters.AddWithValue("@location", (object?)model.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@branch", (object?)model.Branch ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@qty", model.Qty);
        cmd.Parameters.AddWithValue("@customer", (object?)model.Customer ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@notes", (object?)model.Notes ?? DBNull.Value);

        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No stock sparepart found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteStockSparepart(long id)
    {
        string sql = "DELETE FROM stock_sparepart WHERE id = @id";
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

        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No stock sparepart found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Model>> GetStockSpareparts()
    {
        List<Model> items = new();
        string sql = @"
            SELECT id, sparepart, location, branch, qty, customer, notes, created_at
            FROM stock_sparepart
            ORDER BY created_at DESC
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                items.Add(new Model
                {
                    Id = reader.GetInt64(0),
                    Sparepart = reader.GetString(1),
                    Location = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Branch = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Qty = reader.GetInt32(4),
                    Customer = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                    CreatedAt = reader.GetDateTime(7)
                });
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return items;
    }

    public async Task<Model?> GetStockSparepartById(long id)
    {
        Model? model = null;
        string sql = @"
            SELECT id, sparepart, location, branch, qty, customer, notes, created_at
            FROM stock_sparepart
            WHERE id = @id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@id", id);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new Model
                {
                    Id = reader.GetInt64(0),
                    Sparepart = reader.GetString(1),
                    Location = reader.IsDBNull(2) ? null : reader.GetString(2),
                    Branch = reader.IsDBNull(3) ? null : reader.GetString(3),
                    Qty = reader.GetInt32(4),
                    Customer = reader.IsDBNull(5) ? null : reader.GetString(5),
                    Notes = reader.IsDBNull(6) ? null : reader.GetString(6),
                    CreatedAt = reader.GetDateTime(7)
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
