using Npgsql;

namespace StockPrinter;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<Model> CreateStockPrinter(Model model)
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
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
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
        
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Model
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
        else
        {
            throw new Exception("No stock printer found");
        }
    }

    public async Task<Model> UpdateStockPrinter(string mpNo, Model model)
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

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            if (await reader.ReadAsync())
            {
                return new Model
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
            else
            {
                throw new Exception("No stock printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<(List<Model> items, int totalCount)> GetStockPrinters(int page = 1, int pageSize = 10)
    {

        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100); 
        
        int offset = (page - 1) * pageSize;

        List<Model> stockPrinters = new();
        int totalCount = 0;

        using NpgsqlConnection conn = new(_connString);
        try
        {
            await conn.OpenAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }

        string countSql = "SELECT COUNT(1) FROM stock_printer";
        using (NpgsqlCommand countCmd = new(countSql, conn))
        {
            totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
        }

        string dataSql = @"
            SELECT mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes 
            FROM stock_printer 
            ORDER BY mp_no 
            LIMIT @pageSize OFFSET @offset";
            
        using NpgsqlCommand cmd = new(dataSql, conn);
        cmd.Parameters.AddWithValue("@pageSize", pageSize);
        cmd.Parameters.AddWithValue("@offset", offset);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
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

        return (stockPrinters, totalCount);
    }

    public async Task<Model> GetStockPrinterByMPNo(string mpNo)
    {
        string sql = "SELECT mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes FROM stock_printer WHERE mp_no = @mpno";
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
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            if (await reader.ReadAsync())
            {
                return new Model
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
            else
            {
                throw new Exception("No stock printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<Model> DisableStockPrinter(string mpNo)
    {
        string sql = @"
            UPDATE stock_printer SET active = false WHERE mp_no = @mpno
            RETURNING mp_no, printer, serial_no, feature, buy_date, status, location, branch, active, notes
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
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            if (await reader.ReadAsync())
            {
                return new Model
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
            else
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
