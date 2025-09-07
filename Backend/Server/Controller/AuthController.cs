using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Server.Response;
using Npgsql;

namespace Server.Controller
{
    public class LoginRequest
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
    }

    [Route("api/auth")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly string _connString;

        public AuthController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest? request)
        {

            if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new Response<string?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = "Username or password invalid"
                });
            }

         
            string username = request.Username;
            string password = request.Password;

            string sql = "SELECT id, username, role FROM user WHERE username = @username and password = @password";

            using NpgsqlConnection conn = new(_connString);
            using NpgsqlCommand cmd = new(sql, conn);

            try
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", password);

                using NpgsqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    return Unauthorized(new Response<string?>
                    {
                        StatusCode = 401,
                        Ok = false,
                        Data = null,
                        Error = "Authentication failed. Wrong username or password."
                    });
                }

                reader.Read();

                return Ok(new Response<object?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = new
                    {
                        Menu = new
                        {
                            Setting = true,
                            Activity = true,
                            Stock = true,
                            Log = true,
                            Dashboard = true
                        }
                    },
                    Error = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = $"Database connection error. {ex.Message}"
                });
            }
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = "Logged out successfully",
                Error = null
            });
        }

    }
}
