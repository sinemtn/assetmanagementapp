using Microsoft.AspNetCore.Mvc;
using Assignment;
using Server.Response;

namespace Server.Controller
{
    [Route("api/assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly string _connString;

        public AssignmentController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAssignments([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                Service service = new(_connString);
                var (assignments, totalCount) = await service.GetAssignments(page, pageSize);
                var pagination = new Pagination
                {
                    CurrentPage = page,
                    PageSize = pageSize,
                    TotalItems = totalCount
                };
                return Ok(new Response<List<AssignmentModel>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = assignments,
                    Error = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<List<AssignmentModel>?>
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
        public async Task<IActionResult> GetAssignmentByNo(string id)
        {
            AssignmentDetailModel? assignment = null;
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Surat jalan tidak ditemukan"
                    }
                });
            }
            Service service = new(_connString);
            try
            {
                assignment = await service.GetAssignmentByNo(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<AssignmentDetailModel?>
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
            if (assignment == null)
            {
                return NotFound(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Surat jalan tidak ditemukan"
                    }
                });
            }
            return Ok(new Response<AssignmentDetailModel?>
            {
                StatusCode = 200,
                Ok = true,
                Data = assignment,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateAssignment([FromBody] AssignmentDetailModel model)
        {
            if (model == null)
            {
                return BadRequest(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Data surat jalan tidak valid"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                var id = await service.CreateAssignment(model);
                model.AssignmentNo = id;
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate key value"))
            {
                return Conflict(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 409,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Surat jalan dengan ID yang sama sudah ada"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<AssignmentDetailModel?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            
            return Ok(new Response<AssignmentDetailModel?>
            {
                StatusCode = 201,
                Ok = true,
                Data = model,
                Error = null
            });


        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssignment(string id, [FromBody] AssignmentDetailModel model)
        {

            if (model == null || string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Data surat jalan tidak valid atau ID kosong"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateAssignment(id, model);
                model.AssignmentNo = id;
            }
            catch (Exception ex) when (ex.Message.Contains("Surat jalan tidak ditemukan"))
            {
                return NotFound(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Surat jalan tidak ditemukan"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<AssignmentDetailModel?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }
            return Ok(new Response<AssignmentDetailModel?>
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
                return NotFound(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Surat jalan tidak ditemukan"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeleteAssignment(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No assignment found"))
            {
                return NotFound(new Response<AssignmentDetailModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "Surat jalan tidak ditemukan"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<AssignmentDetailModel?>
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
