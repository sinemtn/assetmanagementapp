using Npgsql;

namespace StockToner;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<Model> CreateStockToner(Model model)
    {
        string sql = @"
            INSERT INTO stock_toner (toner, location, branch, qty, customer, notes) 
            SELECT @toner, @location, @branch, @qty, @customer, @notes
            WHERE EXISTS (SELECT 1 FROM ms_toner WHERE toner_id = @toner)
            RETURNING created_at
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

        cmd.Parameters.AddWithValue("@toner", model.Toner);
        cmd.Parameters.AddWithValue("@location", (object?)model.Location ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@branch", (object?)model.Branch ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@qty", model.Qty);
        cmd.Parameters.AddWithValue("@customer", (object?)model.Customer ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@notes", (object?)model.Note ?? DBNull.Value);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            model.CreatedAt = reader.GetDateTime(0);
            return model;
        }
       
        throw new KeyNotFoundException("Toner does not exist in master data.");
        
        
    }

    public async Task<List<Model>> GetStockToners()
    {
        List<Model> stockToners = new();
        string sql = "SELECT toner, location, branch, qty, customer, notes FROM stock_toner";
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

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            stockToners.Add(new Model
            {
                Toner = reader.GetString(0),
                Location = reader.GetString(1),
                Branch = reader.GetString(2),
                Qty = reader.GetInt32(3),
                Customer = reader.IsDBNull(4) ? null : reader.GetString(4),
                Note = reader.IsDBNull(5) ? null : reader.GetString(5)
            });
        }
        
        return stockToners;
    }

    public async Task<Model?> GetStockTonerByToner(string toner)
    {
        string sql = "SELECT toner, location, branch, qty, customer, notes FROM stock_toner WHERE toner = @toner";
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

        cmd.Parameters.AddWithValue("@toner", toner);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Model
            {
                Toner = reader.GetString(0),
                Location = reader.GetString(1),
                Branch = reader.GetString(2),
                Qty = reader.GetInt32(3),
                Customer = reader.IsDBNull(4) ? null : reader.GetString(4),
                Note = reader.IsDBNull(5) ? null : reader.GetString(5)
            };
        }

        throw new KeyNotFoundException("Toner not found.");
    }
}
