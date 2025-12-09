using Npgsql;

namespace Customer;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<int> CreateCustomer(Model model)
    {
        string sql = @"
            INSERT INTO ms_customer (name, address, billing_account) 
            VALUES (@name, @address, @billingaccount)
            RETURNING customer_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@name", model.Name);
            cmd.Parameters.AddWithValue("@address", model.Address);
            cmd.Parameters.AddWithValue("@billingaccount", model.BillingAccount);
            var result = await cmd.ExecuteScalarAsync();
            if (result == null) throw new Exception("Failed to retrieve inserted customer ID");
            var id = (int)result;

            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task UpdateCustomer(int customerId, Model model)
    {
        string sql = @"
            UPDATE ms_customer
            SET name = @name, address = @address, billing_account = @billingaccount
            WHERE customer_id = @customerid
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

        cmd.Parameters.AddWithValue("@customerid", customerId);
        cmd.Parameters.AddWithValue("@name", model.Name);
        cmd.Parameters.AddWithValue("@address", model.Address);
        cmd.Parameters.AddWithValue("@billingaccount", model.BillingAccount);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No customer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteCustomer(int customerId)
    {
        string sql = "DELETE FROM ms_customer WHERE customer_id = @customerid";
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

        cmd.Parameters.AddWithValue("@customerid", customerId);
        try
        {
            var count = await cmd.ExecuteNonQueryAsync();
            if (count == 0)
            {
                throw new Exception("No customer found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }

    }

    public async Task<List<Model>> GetCustomers()
    {
        List<Model> customers = new();
        string sql = "SELECT customer_id, name, address, billing_account FROM ms_customer ORDER BY customer_id";
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
                    CustomerId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2),
                    BillingAccount = reader.GetInt32(3)
                };
                customers.Add(model);
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
        return customers;
    }

    public async Task<Model?> GetCustomerById(int customerId)
    {
        Model? model = null;
        string sql = "SELECT customer_id, name, address, billing_account FROM ms_customer WHERE customer_id = @customerid";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@customerid", customerId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                model = new()
                {
                    CustomerId = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Address = reader.GetString(2),
                    BillingAccount = reader.GetInt32(3)
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