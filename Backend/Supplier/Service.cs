using Npgsql;

namespace Supplier;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<Model> CreateSupplier(Model model)
    {
        string sql = @"
            INSERT INTO supplier (supplier_id, name, address) VALUES (@id, @name, @address) returning supplier_id, name, address
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
        
        cmd.Parameters.AddWithValue("@id", model.SupplierId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@address", model.Address);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        if (await reader.ReadAsync())
        {
            return new Model
            {
                SupplierId = reader.GetString(0),
                Name = reader.GetString(1),
                Address = reader.GetString(2)
            };
        }
        else
        {
            throw new Exception("Supplier gagal dibuat");
        }
    }

    public async Task<Model> UpdateSupplier(string supplierId, Model model)
    {
        string sql = @"
            UPDATE ms_supplier SET name = @name, address = @address WHERE supplier_id = @id
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

        cmd.Parameters.AddWithValue("@id", supplierId);
        cmd.Parameters.AddWithValue("@name", model.Name);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            if (await reader.ReadAsync())
            {
                return new Model
                {
                    SupplierId = reader.GetString(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2)
                };
            }
            else
            {
                throw new Exception("Supplier failed to be updated");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<(List<Model> items, int totalCount)> GetSuppliers(int page = 1, int pageSize = 10)
    {

        page = Math.Max(1, page);
        pageSize = Math.Clamp(pageSize, 1, 100); 
        
        int offset = (page - 1) * pageSize;

        List<Model> suppliers = new();
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

        string countSql = "SELECT COUNT(1) FROM supplier";
        using (NpgsqlCommand countCmd = new(countSql, conn))
        {
            totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
        }

        string dataSql = @"
            SELECT supplier_id, name 
            FROM supplier 
            ORDER BY supplier_id 
            LIMIT @pageSize OFFSET @offset";
            
        using NpgsqlCommand cmd = new(dataSql, conn);
        cmd.Parameters.AddWithValue("@pageSize", pageSize);
        cmd.Parameters.AddWithValue("@offset", offset);

        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            while (await reader.ReadAsync())
            {
                suppliers.Add(new Model
                {
                    SupplierId = reader.GetString(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2)
                });
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }

        return (suppliers, totalCount);
    }

    public async Task<Model> GetSupplierById(string supplierId)
    {
        string sql = "SELECT supplier_id, name, address FROM supplier WHERE supplier_id = @supplierId";
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

        cmd.Parameters.AddWithValue("@supplierId", supplierId);
        using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
        try
        {
            if (await reader.ReadAsync())
            {
                return new Model
                {
                    SupplierId = reader.GetString(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2)
                };
            }
            else
            {
                throw new Exception("Supplier not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task DeleteSupplier(string supplierId)
    {
        string sql = "DELETE FROM supplier WHERE supplier_id = @supplierId";
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

        cmd.Parameters.AddWithValue("@supplierId", supplierId);
        try
        {
            int affectedRows = await cmd.ExecuteNonQueryAsync();
            if (affectedRows == 0)
            {
                throw new Exception("Supplier not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }
}
