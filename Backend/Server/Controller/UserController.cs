using Microsoft.AspNetCore.Mvc;

namespace Server.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUsers()
        {
            // You can access user information from the authenticated context
            var username = User.Identity?.Name;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;
            
            return Ok(new { 
                Message = "Authenticated successfully", 
                Username = username, 
                Role = role 
            });
        }

        [HttpPost]
        public IActionResult CreateUser()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateUser()
        {
            return Ok();
        }
    }
}


