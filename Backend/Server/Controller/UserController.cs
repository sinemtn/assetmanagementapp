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
            return Ok();
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


