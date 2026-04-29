using Npgsql;

namespace Complaint;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<(List<ComplaintModel>, int)> GetComplaints(int page, int pageSize)
    {
        List<ComplaintModel> complaints = new();
        string sql = @"
            SELECT c.complaint_id, c.mp_no, c.description, c.customer, cust.name, c.sales, c.status,
                   c.resolved_at, c.created_at, c.updated_at
            FROM complaint c
            LEFT JOIN ms_customer cust ON c.customer = cust.customer_id
            ORDER BY c.created_at DESC
            LIMIT @pageSize OFFSET @offset
        ";
        string countSql = "SELECT COUNT(*) FROM complaint";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        using NpgsqlCommand countCmd = new(countSql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@pageSize", pageSize);
            cmd.Parameters.AddWithValue("@offset", (page - 1) * pageSize);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                complaints.Add(new ComplaintModel
                {
                    ComplaintNo = reader.GetString(0),
                    MPNo = reader.GetString(1),
                    Description = reader.GetString(2),
                    Customer = new Customer.Model
                    {
                        CustomerId = reader.GetString(3),
                        Name = reader.IsDBNull(4) ? null : reader.GetString(4)
                    },
                    Sales = reader.GetString(5),
                    Status = reader.GetString(6)
                });
            }
            reader.Close();
            int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
            return (complaints, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<ComplaintModel> GetComplaintById(string complaintId)
    {
        string sql = @"
            SELECT c.complaint_id, c.mp_no, c.description, c.customer, cust.name, c.sales, c.status,
                   c.resolved_at, c.created_at, c.updated_at
            FROM complaint c
            LEFT JOIN ms_customer cust ON c.customer = cust.customer_id
            WHERE c.complaint_id = @complaintId
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintId", complaintId);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ComplaintModel
                {
                    ComplaintNo = reader.GetString(0),
                    MPNo = reader.GetString(1),
                    Description = reader.GetString(2),
                    Customer = new Customer.Model
                    {
                        CustomerId = reader.GetString(3),
                        Name = reader.IsDBNull(4) ? null : reader.GetString(4)
                    },
                    Sales = reader.GetString(5),
                    Status = reader.GetString(6)
                };
            }
            throw new Exception("Complaint not found");
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<string> CreateComplaint(ComplaintModel model)
    {
        string sql = @"
            INSERT INTO complaint (
                mp_no, description, customer, sales, status,
                created_at, updated_at
            ) VALUES (
                @mpNo, @description, @customer, @sales, @status,
                NOW(), NOW()
            )
            RETURNING complaint_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@mpNo", model.MPNo);
            cmd.Parameters.AddWithValue("@description", model.Description);
            cmd.Parameters.AddWithValue("@customer", model.Customer?.CustomerId ?? string.Empty);
            cmd.Parameters.AddWithValue("@sales", model.Sales);
            cmd.Parameters.AddWithValue("@status", model.Status);

            var result = await cmd.ExecuteScalarAsync() ?? throw new Exception("Failed to retrieve inserted complaint ID");
            return (string)result;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<ComplaintModel> UpdateComplaint(string complaintId, ComplaintModel model)
    {
        string sql = @"
            UPDATE complaint SET
                mp_no = @mpNo,
                description = @description,
                customer = @customer,
                sales = @sales,
                status = @status,
                updated_at = NOW()
            WHERE complaint_id = @complaintId
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintId", complaintId);
            cmd.Parameters.AddWithValue("@mpNo", model.MPNo);
            cmd.Parameters.AddWithValue("@description", model.Description);
            cmd.Parameters.AddWithValue("@customer", model.Customer?.CustomerId ?? string.Empty);
            cmd.Parameters.AddWithValue("@sales", model.Sales);
            cmd.Parameters.AddWithValue("@status", model.Status);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
                throw new Exception("Complaint not found");

            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task ChangeStatus(string complaintId, string newStatus)
    {
        var validStatuses = new[] { "new", "in progress", "resolved"};
        if (!validStatuses.Contains(newStatus))
            throw new ArgumentException("Invalid status value");

        string sql = @"
            UPDATE complaint SET
                status = @status,
                updated_at = NOW(),
                resolved_at = CASE WHEN @status = 'resolved' THEN NOW() ELSE resolved_at END
            WHERE complaint_id = @complaintId
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@status", newStatus);
            cmd.Parameters.AddWithValue("@complaintId", complaintId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
                throw new Exception("Complaint not found");
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task DeleteComplaint(string complaintId)
    {
        string sql = "DELETE FROM complaint WHERE complaint_id = @complaintId";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintId", complaintId);

            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
                throw new Exception("No complaint found to delete");
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }
}