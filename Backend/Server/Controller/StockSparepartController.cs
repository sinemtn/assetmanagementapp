using Microsoft.AspNetCore.Mvc;
using StockSparepart;
using Server.Response;

namespace Server.Controller
{
    [Route("api/stock/sparepart")]
    [ApiController]
    public class StockSparepartController : ControllerBase
    {
        private readonly string _connString;

        public StockSparepartController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
        }

        [HttpGet]
        public async Task<IActionResult> GetStockSpareparts()
        {
            try
            {
                Service service = new(_connString);
                var items = await service.GetStockSpareparts();
                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = items,
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
        public async Task<IActionResult> GetStockSparepartById(long id)
        {
            Service service = new(_connString);
            try
            {
                var item = await service.GetStockSparepartById(id);
                if (item == null)
                {
                    return NotFound(new Response<Model?>
                    {
                        StatusCode = 404,
                        Ok = false,
                        Data = null,
                        Error = new { message = "Stock sparepart tidak ditemukan" }
                    });
                }
                return Ok(new Response<Model?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = item,
                    Error = null
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
        }

        [HttpPost]
        public async Task<IActionResult> CreateStockSparepart([FromBody] Model model)
        {
            if (model == null)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Data stock sparepart tidak valid" }
                });
            }

            Service service = new(_connString);
            try
            {
                var result = await service.CreateStockSparepart(model);
                return Ok(new Response<Model?>
                {
                    StatusCode = 201,
                    Ok = true,
                    Data = result,
                    Error = null
                });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
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
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStockSparepart(long id, [FromBody] Model model)
        {
            if (model == null)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Data stock sparepart tidak valid" }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateStockSparepart(id, model);
                model.Id = id;
                return Ok(new Response<Model?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = model,
                    Error = null
                });
            }
            catch (Exception ex) when (ex.Message.Contains("No stock sparepart found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Stock sparepart tidak ditemukan" }
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
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStockSparepart(long id)
        {
            Service service = new(_connString);
            try
            {
                await service.DeleteStockSparepart(id);
                return Ok(new Response<string?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = "Berhasil menghapus stock sparepart",
                    Error = null
                });
            }
            catch (Exception ex) when (ex.Message.Contains("No stock sparepart found"))
            {
                return NotFound(new Response<string?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new { message = "Stock sparepart tidak ditemukan" }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<string?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
        }
    }
}
