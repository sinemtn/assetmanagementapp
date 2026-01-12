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
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
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
                    Error = new { message = "Username atau password tidak valid" }
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
                        Error = new { message = "Autentikasi gagal. Username atau password salah." }
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
                    Error = new { message = $"Database connection error. {ex.Message}" }
                });
            }

            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = "Berhasil login",
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
                Data = "Berhasil logout",
                Error = null
            });
        }

    }
}
