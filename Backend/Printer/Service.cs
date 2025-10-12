using Npgsql;

namespace Printer;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task CreatePrinter(Model model)
    {
        string sql = @"
            INSERT INTO ms_printer (printer_id, name, manufacture, category, toner, active) 
            VALUES (@printerid, @name, @manufacture, @category, @toner, @active)
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@printerid", model.PrinterID);
            cmd.Parameters.AddWithValue("@name", model.Name);
            cmd.Parameters.AddWithValue("@manufacture", model.Manufacture);
            cmd.Parameters.AddWithValue("@category", model.Category);
            cmd.Parameters.AddWithValue("@toner", model.Toner);
            cmd.Parameters.AddWithValue("@active", model.Active);
            await cmd.ExecuteNonQueryAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdatePrinter(string printerId, Model model)
    {
        string sql = @"
            UPDATE ms_printer
            SET name = @name, manufacture = @manufacture, category = @category, toner = @toner, active = @active
            WHERE printer_id = @printerid
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

        cmd.Parameters.AddWithValue("@printerid", printerId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@manufacture", model.Manufacture);
        cmd.Parameters.AddWithValue("@category", model.Category);
        cmd.Parameters.AddWithValue("@toner", model.Toner);
        cmd.Parameters.AddWithValue("@active", model.Active);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeletePrinter(string printerId)
    {
        string sql = "DELETE FROM ms_printer WHERE printer_id = @printerid";
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

        cmd.Parameters.AddWithValue("@printerid", printerId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No printer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetPrinters()
    {
        List<Model> printers = new();
        string sql = "SELECT printer_id, name, manufacture, category, toner, active FROM ms_printer ORDER BY printer_id";
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
                    PrinterID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Manufacture = reader.GetString(2),
                    Category = reader.GetString(3),
                    Toner = reader.GetString(4),
                    Active = reader.GetBoolean(5)
                };
                printers.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return printers;
    }

    public async Task<Model?> GetPrinterById(string printerId)
    {
        Model? model = null;
        string sql = "SELECT printer_id, name, manufacture, category, toner, active FROM ms_printer WHERE printer_id = @printerid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@printerid", printerId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    PrinterID = reader.GetString(0),
                    Name = reader.GetString(1),
                    Manufacture = reader.GetString(2),
                    Category = reader.GetString(3),
                    Toner = reader.GetString(4),
                    Active = reader.GetBoolean(5)
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
