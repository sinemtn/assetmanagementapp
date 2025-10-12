using Microsoft.AspNetCore.Mvc;

namespace Server.Controller
{
    [Route("api/toner")]
    [ApiController]
    public class TonerController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetToners()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateToner()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateToner()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteToner()
        {
            return Ok();
        }
    }
}
