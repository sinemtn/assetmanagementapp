using Npgsql;

namespace StockPrinter;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task CreateStockPrinter(Model model)
    {
        string sql = @"
            INSERT INTO stock_printer (mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes) 
            VALUES (@mpno, @printer, @serialno, @feature, @buydate, @status, @location, @branch, @active, @note)
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@mpno", model.MPNo);
            cmd.Parameters.AddWithValue("@printer", model.Printer);
            cmd.Parameters.AddWithValue("@serialno", model.SerialNo);
            cmd.Parameters.AddWithValue("@feature", (object?)model.Feature ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@buydate", model.BuyDate);
            cmd.Parameters.AddWithValue("@location", model.Location);
            cmd.Parameters.AddWithValue("@branch", model.Branch);
            cmd.Parameters.AddWithValue("@status", model.Status);
            cmd.Parameters.AddWithValue("@active", model.Active);
            cmd.Parameters.AddWithValue("@note", (object?)model.Note ?? DBNull.Value);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateStockPrinter(string mpNo, Model model)
    {
        string sql = @"
            UPDATE stock_printer
            SET 
                printer = @printer, 
                serial_no = @serialno, 
                feature = @feature, 
                buy_date = @buydate, 
                status = @status,
                location = @location, 
                branch = @branch,
                active = @active, 
                notes = @note
            WHERE mp_no = @mpno
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

        cmd.Parameters.AddWithValue("@mpno", mpNo);
        cmd.Parameters.AddWithValue("@printer", model.Printer);
        cmd.Parameters.AddWithValue("@serialno", model.SerialNo);
        cmd.Parameters.AddWithValue("@feature", (object?)model.Feature ?? DBNull.Value);
        cmd.Parameters.AddWithValue("@buydate", model.BuyDate);
        cmd.Parameters.AddWithValue("@location", model.Location);
        cmd.Parameters.AddWithValue("@branch", model.Branch);
        cmd.Parameters.AddWithValue("@status", model.Status);
        cmd.Parameters.AddWithValue("@active", model.Active);
        cmd.Parameters.AddWithValue("@note", (object?)model.Note ?? DBNull.Value);

        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No stock printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task<List<Model>> GetStockPrinters()
    {
        List<Model> stockPrinters = new();
        string sql = "SELECT mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes FROM stock_printer";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                stockPrinters.Add(new Model
                {
                    MPNo = reader.GetString(0),
                    Printer = reader.GetString(1),
                    SerialNo = reader.GetString(2),
                    Feature = reader.IsDBNull(3) ? null : reader.GetString(3),
                    BuyDate = reader.GetDateTime(4),
                    Status = reader.GetString(5),
                    Location = reader.GetString(6),
                    Branch = reader.GetString(7),
                    Active = reader.GetBoolean(8),
                    Note = reader.IsDBNull(9) ? null : reader.GetString(9)
                });
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return stockPrinters;
    }

    public async Task<Model?> GetStockPrinterByMPNo(string mpNo)
    {
        Model? model = null;
        string sql = "SELECT mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes FROM stock_printer WHERE mp_no = @mpno";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@mpno", mpNo);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new Model
                {
                    MPNo = reader.GetString(0),
                    Printer = reader.GetString(1),
                    SerialNo = reader.GetString(2),
                    Feature = reader.IsDBNull(3) ? null : reader.GetString(3),
                    BuyDate = reader.GetDateTime(4),
                    Status = reader.GetString(5),
                    Location = reader.GetString(6),
                    Branch = reader.GetString(7),
                    Active = reader.GetBoolean(8),
                    Note = reader.IsDBNull(9) ? null : reader.GetString(9)
                };
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return model;
    }

    public async Task DisableStockPrinter(string mpNo)
    {
        string sql = "UPDATE stock_printer SET active = false WHERE mp_no = @mpno";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@mpno", mpNo);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("No stock printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }
}
