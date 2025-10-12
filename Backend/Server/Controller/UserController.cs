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
            var username = User.Identity?.Name;
            var role = User.FindFirst(System.Security.Claims.ClaimTypes.Role)?.Value;

            return Ok(new
            {
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
        
        [HttpDelete]
        public IActionResult DeleteUser()
        {
            return Ok();
        }
    }
}


