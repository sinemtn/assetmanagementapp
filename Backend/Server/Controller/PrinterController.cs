using Microsoft.AspNetCore.Mvc;
using Printer;
using Server.Response;

namespace Server.Controller
{
    [Route("api/printer")]
    [ApiController]
    public class PrinterController : ControllerBase
    {
        private readonly string _connString;

        public PrinterController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetPrinters()
        {
            try
            {
                Service service = new(_connString);
                var printers = await service.GetPrinters();
                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = printers,
                    Error = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<Model>?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPrinterById(string id)
        {
            Model? printer = null;
             if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer ID not found" }
                });
            }
            Service service = new(_connString);
            try
            {
                printer = await service.GetPrinterById(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            if (printer == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer not found" }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = printer,
                Error = null
            });
            
        }


        [HttpPost]
        public async Task<IActionResult> CreatePrinter([FromBody] Model model)
        {

            if (model == null)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Invalid printer data" }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.CreatePrinter(model);
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate key value"))
            {
                return Conflict(new Response<Model?>
                {
                    StatusCode = 409,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer with the same ID already exists" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            

            return Ok(new Response<Model?>
            {
                StatusCode = 201,
                Ok = true,
                Data = model,
                Error = null
            });
            

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrinter(string id, [FromBody] Model model)
        {
            
            if (model == null || string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Invalid printer data or ID" }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdatePrinter(id, model);
            }
            catch (Exception ex) when (ex.Message.Contains("No printer found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer ID not found" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = model,
                Error = null
            });
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrinter(string id)
        {
            
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer ID not found" }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeletePrinter(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No printer found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Printer ID not found" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = "Delete successful",
                Error = null
            });
            
        }
    }
}
