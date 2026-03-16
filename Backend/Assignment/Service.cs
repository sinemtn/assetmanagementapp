using Npgsql;

namespace Assignment;

public class Service
{
    private string _connString;

    public Service(string connString)
    {
        _connString = connString ?? throw new ArgumentNullException(nameof(connString));
    }
    
    public async Task<string> CreateAssignment(AssignmentDetailModel model)
    {
        string sql = @"
            INSERT INTO assignment (
                assignment_no, complaint_no, task, customer, pic, status, has_items,
                validated_at, validated_by, authorized_at, authorized_by,
                created_at, updated_at, mp_no
            ) VALUES (
                nextval('assignment_no_seq'), @complaintNo, @task, @customer, @pic, @status, @hasItems,
                @validatedAt, @validatedBy, @authorizedAt, @authorizedBy,
                NOW(), NOW(), @mpNo
            )
            RETURNING assignment_no
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintNo", model.ComplaintNo);
            cmd.Parameters.AddWithValue("@task", model.Task);
            cmd.Parameters.AddWithValue("@customer", model.Customer);
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

    public async Task<AssignmentDetailModel> UpdateAssignment(string assignmentNo, AssignmentDetailModel model)
    {
        string sql = @"
            UPDATE assignment SET
                complaint_no = @complaintNo,
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
            WHERE assignment_no = @assignmentNo
        ";
        using NpgsqlConnection conn = new(_connString);
        using NpgsqlCommand cmd = new(sql, conn);
        try
        {
            await conn.OpenAsync();
            cmd.Parameters.AddWithValue("@complaintNo", model.ComplaintNo);
            cmd.Parameters.AddWithValue("@task", model.Task);
            cmd.Parameters.AddWithValue("@customer", model.Customer);
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

    public async Task ChangeStatus(string assignmentNo, string newStatus, string changedBy)
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
            WHERE assignment_no = @assignmentNo
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

    public async Task<AssignmentDetailModel> GetAssignmentByNo(string assignmentNo)
    {
        string sql = @"
            SELECT assignment_no, complaint_no, task, customer, pic, status, mp_no,
                   validated_at, validated_by, authorized_at, authorized_by,
                   created_at, updated_at
            FROM assignment
            WHERE assignment_no = @assignmentNo
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
                return new AssignmentDetailModel
                {
                    AssignmentNo = reader.GetString(0),
                    ComplaintNo = reader.GetString(1),
                    Task = reader.GetString(2),
                    Customer = reader.GetString(3),
                    PIC = reader.GetString(4),
                    Status = reader.GetString(5),
                    MPNo = reader.GetString(6),
                    ValidatedAt = reader.IsDBNull(7) ? null : reader.GetDateTime(7),
                    ValidatedBy = reader.IsDBNull(8) ? null : reader.GetString(8),
                    AuthorizedAt = reader.IsDBNull(9) ? null : reader.GetDateTime(9),
                    AuthorizedBy = reader.IsDBNull(10) ? null : reader.GetString(10),
                    CreatedAt = reader.GetDateTime(11),
                    UpdatedAt = reader.GetDateTime(12)
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
            SELECT assignment_no, customer, task, pic, status
            FROM assignment
            ORDER BY created_at DESC
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
                    AssigmentNo = reader.GetString(0),
                    Customer = reader.GetString(1),
                    Task = reader.GetString(2),
                    PIC = reader.GetString(3),
                    Status = reader.GetString(4)
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
        string sql = "DELETE FROM assignment WHERE assignment_no = @assignmentNo";
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
        