using Microsoft.AspNetCore.Mvc;
using StockToner;
using Server.Response;

namespace Server.Controller
{
    [Route("api/stock/toner")]
    [ApiController]
    public class StockTonerController : ControllerBase
    {
        private readonly string _connString;

        public StockTonerController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
        }

        [HttpGet]
        public async Task<IActionResult> GetStockToners()
        {
            try
            {
                Service service = new(_connString);
                var stockToners = await service.GetStockToners();
                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = stockToners,
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
        public async Task<IActionResult> GetStockTonerByToner(string id)
        {
            Model? stockToner;
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Stok toner tidak ditemukan"
                    }
                });
            }
            Service service = new(_connString);
            try
            {
                stockToner = await service.GetStockTonerByToner(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stok toner tidak ditemukan"
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
                    Error = new
                    {
                        message = ex.Message
                    }
                });
            }
            if (stockToner == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Stok toner tidak ditemukan"
                    }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = stockToner,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateStockToner([FromBody] Model model)
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
                        message = "Data stok toner tidak valid"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                model = await service.CreateStockToner(model);
            }
            catch (KeyNotFoundException)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Toner tidak ditemukan dalam data master"
                    }
                });
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
                        message = "Stok toner dengan toner yang sama sudah ada"
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

    }
}
