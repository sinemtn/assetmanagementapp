using Microsoft.AspNetCore.Mvc;
using Sparepart;
using Server.Response;

namespace Server.Controller
{
    [Route("api/sparepart")]
    [ApiController]
    public class SparepartController : ControllerBase
    {
        private readonly string _connString;

        public SparepartController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetSpareparts()
        {
            try
            {
                Service service = new(_connString);
                var spareparts = await service.GetSpareparts();
                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = spareparts,
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
                    Error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSparepartById(string id)
        {
            Model? sparepart = null;
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart ID not found"
                });
            }
            Service service = new(_connString);
            try
            {
                sparepart = await service.GetSparepartById(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = ex.Message
                });
            }
            if (sparepart == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart not found"
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = sparepart,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateSparepart([FromBody] Model model)
        {

            if (model == null)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = "Invalid sparepart data"
                });
            }

            Service service = new(_connString);
            try
            {
                await service.CreateSparepart(model);
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate key value"))
            {
                return Conflict(new Response<Model?>
                {
                    StatusCode = 409,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart with the same ID already exists"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = ex.Message
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSparepart(string id, [FromBody] Model model)
        {

            if (model == null || string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = "Invalid sparepart data or ID"
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateSparepart(id, model);
            }
            catch (Exception ex) when (ex.Message.Contains("No sparepart found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart ID not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = ex.Message
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
        public async Task<IActionResult> DeleteSparepart(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart ID not found"
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeleteSparepart(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No sparepart found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = "Sparepart ID not found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<Model?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = ex.Message
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = null,
                Error = null
            });

        }
    }
}
