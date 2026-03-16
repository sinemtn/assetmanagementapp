using Microsoft.AspNetCore.Mvc;
using Supplier;
using Server.Response;

namespace Server.Controller
{
    [Route("api/supplier")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly string _connString;

        public SupplierController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
        }

        [HttpGet]
        public async Task<IActionResult> GetSuppliers([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                Service service = new(_connString);
                var (suppliers, totalCount) = await service.GetSuppliers(page, pageSize);
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
                    Data = suppliers,
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
        public async Task<IActionResult> GetSupplierById(string id)
        {
            Model? supplier = null;
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Supplier tidak ditemukan"
                    }
                });
            }
            Service service = new(_connString);
            try
            {
                supplier = await service.GetSupplierById(id);
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
            if (supplier == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Supplier tidak ditemukan"
                    }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = supplier,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateSupplier([FromBody] Model model)
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
                        message = "Data supplier tidak valid"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                var supplier = await service.CreateSupplier(model);
                model.SupplierId = supplier.SupplierId;
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
                        message = "Supplier dengan ID yang sama sudah ada"
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
        public async Task<IActionResult> UpdateSupplier(string id, [FromBody] Model model)
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
                        message = "Data supplier tidak valid atau ID kosong"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateSupplier(id, model);
                model.SupplierId = id;
            }
            catch (Exception ex) when (ex.Message.Contains("Supplier tidak ditemukan"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Supplier tidak ditemukan"
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSupplier(string id)
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
                        message = "Supplier tidak ditemukan"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeleteSupplier(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No supplier found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Supplier tidak ditemukan"
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
                Data = "Berhasil menghapus supplier",
                Error = null
            });

        }
    }
}
