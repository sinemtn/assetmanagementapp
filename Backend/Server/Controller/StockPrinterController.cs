using Microsoft.AspNetCore.Mvc;
using StockPrinter;
using Server.Response;

namespace Server.Controller
{
    [Route("api/stock/printer")]
    [ApiController]
    public class StockPrinterController : ControllerBase
    {
        private readonly string _connString;

        public StockPrinterController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetStockPrinters([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                Service service = new(_connString);
                var (stockPrinters, totalCount) = await service.GetStockPrinters(page, pageSize);
                
                var pagination = new Pagination
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalCount
                };

                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = stockPrinters,
                    Pagination = pagination,
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
                    Error = new 
                    {
                        message = ex.Message
                    }
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockPrinterById(string id)
        {
            Model? stockPrinter = null;
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Stock printer not found"
                    }
                });
            }
            Service service = new(_connString);
            try
            {
                stockPrinter = await service.GetStockPrinterByMPNo(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = ex.Message
                    }
                });
            }
            if (stockPrinter == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stock printer not found"
                    }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = stockPrinter,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateStockPrinter([FromBody] Model model)
        {

            if (model == null)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Invalid stock printer data"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                model = await service.CreateStockPrinter(model);
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate key value"))
            {
                return Conflict(new Response<Model?>
                {
                    StatusCode = 409,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stock printer with the same ID already exists"
                    }
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
        public async Task<IActionResult> UpdateStockPrinter(string id, [FromBody] Model model)
        {

            if (model == null || string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Invalid stock printer data or ID"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                model = await service.UpdateStockPrinter(id, model);
            }
            catch (Exception ex) when (ex.Message.Contains("No stock printer found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stock printer not found"
                    }
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> DisableStockPrinter(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Stock printer not found"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DisableStockPrinter(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No stock printer found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stock printer not found"
                    }
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
                Data = "Patching successful",
                Error = null
            });

        }
    }
}
