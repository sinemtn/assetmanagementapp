using Npgsql;

namespace Complaint;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }

    public async Task<string> CreateComplaint(ComplaintModel model)
    {
        string sql = @"
            INSERT INTO complaint (
                complaint_id, no_mp, description, customer, sales, status,
                created_at, updated_at
            ) VALUES (
                @complaintId, @noMp, @description, @customer, @sales, @status,
                NOW(), NOW()
            )
            RETURNING complaint_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintId", model.ComplaintNo);
            cmd.Parameters.AddWithValue("@noMp", model.MPNo);
            cmd.Parameters.AddWithValue("@description", model.Description);
            cmd.Parameters.AddWithValue("@customer", model.Customer);
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
                no_mp = @noMp,
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
            cmd.Parameters.AddWithValue("@noMp", model.MPNo);
            cmd.Parameters.AddWithValue("@description", model.Description);
            cmd.Parameters.AddWithValue("@customer", model.Customer);
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