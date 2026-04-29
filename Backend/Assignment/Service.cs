using Npgsql;

namespace Assignment;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }
    
    public async Task<string> CreateAssignment(AssignmentModel model)
    {
        string sql = @"
            INSERT INTO assignment (
                complaint, task, customer, pic, status, has_items,
                validated_at, validated_by, authorized_at, authorized_by,
                created_at, updated_at, mp_no
            ) VALUES (
                @complaintNo, @task, @customer, @pic, @status, @hasItems,
                @validatedAt, @validatedBy, @authorizedAt, @authorizedBy,
                NOW(), NOW(), @mpNo
            )
            RETURNING assigment_id
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintNo", model.ComplaintNo);
            cmd.Parameters.AddWithValue("@task", model.Task);
            cmd.Parameters.AddWithValue("@customer", model.Customer?.CustomerId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@pic", model.PIC);
            cmd.Parameters.AddWithValue("@status", model.Status);
            cmd.Parameters.AddWithValue("@hasItems", model.Items.Length > 0);
            cmd.Parameters.AddWithValue("@validatedAt", model.ValidatedAt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@validatedBy", model.ValidatedBy ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@authorizedAt", model.AuthorizedAt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@authorizedBy", model.AuthorizedBy ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@mpNo", model.MPNo);
            var result = await cmd.ExecuteScalarAsync() ?? throw new Exception("Failed to retrieve inserted assignment ID");
            var id = (string)result;

            return id;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<AssignmentModel> UpdateAssignment(string assignmentNo, AssignmentModel model)
    {
        string sql = @"
            UPDATE assignment SET
                complaint = @complaintNo,
                task = @task,
                customer = @customer,
                pic = @pic,
                status = @status,
                has_items = @hasItems,
                validated_at = @validatedAt,
                validated_by = @validatedBy,
                authorized_at = @authorizedAt,
                authorized_by = @authorizedBy,
                updated_at = NOW()
            WHERE assigment_id = @assignmentNo
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintNo", model.ComplaintNo);
            cmd.Parameters.AddWithValue("@task", model.Task);
            cmd.Parameters.AddWithValue("@customer", model.Customer?.CustomerId ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@pic", model.PIC);
            cmd.Parameters.AddWithValue("@status", model.Status);
            cmd.Parameters.AddWithValue("@hasItems", model.Items.Length > 0);
            cmd.Parameters.AddWithValue("@validatedAt", model.ValidatedAt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@validatedBy", model.ValidatedBy ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@authorizedAt", model.AuthorizedAt ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@authorizedBy", model.AuthorizedBy ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@assignmentNo", assignmentNo);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("Assignment not found");
            }

            return model;
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task ChangeStatus(string assignmentNo, string newStatus, string changedBy = "")
    {
        var validStatuses = new[] { "pending", "in progress", "completed", "validated", "authorized" };
        if (!validStatuses.Contains(newStatus))
            throw new ArgumentException("Invalid status value");       

        string sql = @"
            UPDATE assignment SET
                status = @status,
                updated_at = NOW(), 
                validated_at = CASE WHEN @status = 'validated' THEN NOW() ELSE validated_at END,
                validated_by = CASE WHEN @status = 'validated' THEN @changedBy ELSE validated_by END,
                authorized_at = CASE WHEN @status = 'authorized' THEN NOW() ELSE authorized_at END,
                authorized_by = CASE WHEN @status = 'authorized' THEN @changedBy ELSE authorized_by END
            WHERE assigment_id = @assignmentNo
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@status", newStatus);
            cmd.Parameters.AddWithValue("@changedBy", changedBy);
            cmd.Parameters.AddWithValue("@assignmentNo", assignmentNo);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("Assignment not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<AssignmentModel> GetAssignmentByNo(string assignmentNo)
    {
        string sql = @"
            SELECT a.assigment_id, a.complaint, a.task, a.customer, c.name, c.address, c.billing_account, a.pic, a.status, a.mp_no,
                   a.validated_at, a.validated_by, a.authorized_at, a.authorized_by,
                   a.created_at, a.updated_at
            FROM assignment a
            JOIN ms_customer c ON a.customer = c.customer_id
            WHERE a.assigment_id = @assignmentNo
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@assignmentNo", assignmentNo);
            using NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new AssignmentModel
                {
                    AssignmentNo = reader.GetString(0),
                    ComplaintNo = reader.IsDBNull(1) ? string.Empty : reader.GetString(1),
                    Task = reader.GetString(2),
                    Customer = new Customer.Model
                    {
                        CustomerId = reader.GetString(3),
                        Name = reader.GetString(4),
                        Address = reader.IsDBNull(5) ? null : reader.GetString(5),
                        BillingAccount = reader.IsDBNull(6) ? null : reader.GetInt32(6)
                    },
                    PIC = reader.GetString(7),
                    Status = reader.GetString(8),
                    MPNo = reader.GetString(9),
                    ValidatedAt = reader.IsDBNull(10) ? null : reader.GetDateTime(10),
                    ValidatedBy = reader.IsDBNull(11) ? null : reader.GetString(11),
                    AuthorizedAt = reader.IsDBNull(12) ? null : reader.GetDateTime(12),
                    AuthorizedBy = reader.IsDBNull(13) ? null : reader.GetString(13),
                    CreatedAt = reader.GetDateTime(14),
                    UpdatedAt = reader.GetDateTime(15)
                };
            }
            else
            {
                throw new Exception("Assignment not found");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task<(List<AssignmentModel>, int)> GetAssignments(int page, int pageSize)
    {
        List<AssignmentModel> assignments = new();
        string sql = @"
            SELECT a.assigment_id, a.customer, c.name, c.address, c.billing_account, a.task, a.pic, a.status, a.mp_no,
                   a.validated_at, a.validated_by, a.authorized_at, a.authorized_by,
                   a.created_at, a.updated_at
            FROM assignment a
            JOIN ms_customer c ON a.customer = c.customer_id
            ORDER BY a.created_at DESC
            LIMIT @pageSize OFFSET @offset
        ";
        string countSql = "SELECT COUNT(*) FROM assignment";
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
                assignments.Add(new AssignmentModel
                {
                    AssignmentNo = reader.GetString(0),
                    Customer = new Customer.Model
                    {
                        CustomerId = reader.GetString(1),
                        Name = reader.GetString(2),
                        Address = reader.IsDBNull(3) ? null : reader.GetString(3),
                        BillingAccount = reader.IsDBNull(4) ? null : reader.GetInt32(4)
                    },
                    Task = reader.GetString(5),
                    PIC = reader.GetString(6),
                    Status = reader.GetString(7),
                    MPNo = reader.GetString(8),
                    ValidatedAt = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
                    ValidatedBy = reader.IsDBNull(10) ? null : reader.GetString(10),
                    AuthorizedAt = reader.IsDBNull(11) ? null : reader.GetDateTime(11),
                    AuthorizedBy = reader.IsDBNull(12) ? null : reader.GetString(12),
                    CreatedAt = reader.GetDateTime(13),
                    UpdatedAt = reader.GetDateTime(14)
                });
            }
            reader.Close();

            int totalCount = Convert.ToInt32(await countCmd.ExecuteScalarAsync());
            return (assignments, totalCount);
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }

    public async Task DeleteAssignment(string assignmentNo)
    {
        string sql = "DELETE FROM assignment WHERE assigment_id = @assignmentNo";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@assignmentNo", assignmentNo);
            int rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected == 0)
            {
                throw new Exception("No assignment found to delete");
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"Database error: {ex.Message}");
        }
    }
}
        