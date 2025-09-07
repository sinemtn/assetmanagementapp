using Microsoft.AspNetCore.Mvc;

namespace Server.Controller
{
    [Route("api/printer")]
    [ApiController]
    public class PrinterController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPrinters()
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreatePrinter()
        {
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdatePrinter()
        {
            return Ok();
        }
    }
}
