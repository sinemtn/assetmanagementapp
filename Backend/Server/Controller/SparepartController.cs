using Microsoft.AspNetCore.Mvc;

namespace Server.Controller
{
    [Route("api/sparepart")]
    [ApiController]
    public class SparepartController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetSpareparts()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateSparepart()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateSparepart()
        {
            return Ok();
        }

        [HttpDelete]
        public IActionResult DeleteSparepart()
        {
            return Ok();
        }
    }
}
