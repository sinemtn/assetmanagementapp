using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json.Serialization;
using Server.Response;
using Auth;

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

            // Extract username and password from request
            string username = request.Username;
            string password = request.Password;

            try
            {
                Authentication auth = new(_connString);
                bool isAuthenticated = auth.IsAuthenticated(username, password).Result;
                if (!isAuthenticated)
                {
                    return Unauthorized(new Response<string?>
                    {
                        StatusCode = 401,
                        Ok = false,
                        Data = null,
                        Error = "Authentication failed. Wrong username or password."
                    });
                }
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

            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = "Login successful",
                Error = null
            });
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
