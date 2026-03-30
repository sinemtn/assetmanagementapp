using Microsoft.AspNetCore.Mvc;
using Complaint;
using Server.Response;
using Server.Request;

namespace Server.Controller
{
    [Route("api/complaint")]
    [ApiController]
    public class ComplaintController : ControllerBase
    {
        private readonly string _connString;

        public ComplaintController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' tidak ditemukan.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateComplaint([FromBody] ComplaintModel model)
        {
            if (model == null)
            {
                return BadRequest(new Response<ComplaintModel?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Data komplain tidak valid"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                var id = await service.CreateComplaint(model);
                model.ComplaintNo = id;
            }
            catch (Exception ex) when (ex.Message.Contains("duplicate key value"))
            {
                return Conflict(new Response<ComplaintModel?>
                {
                    StatusCode = 409,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Komplain dengan ID yang sama sudah ada"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ComplaintModel?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }

            return Ok(new Response<ComplaintModel?>
            {
                StatusCode = 201,
                Ok = true,
                Data = model,
                Error = null
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComplaint(string id, [FromBody] ComplaintModel model)
        {
            if (model == null || string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<ComplaintModel?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Data komplain tidak valid atau ID kosong"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateComplaint(id, model);
                model.ComplaintNo = id;
            }
            catch (Exception ex) when (ex.Message.Contains("Complaint not found"))
            {
                return NotFound(new Response<ComplaintModel?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Komplain tidak ditemukan"
                    }
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response<ComplaintModel?>
                {
                    StatusCode = 500,
                    Ok = false,
                    Data = null,
                    Error = new { message = ex.Message }
                });
            }

            return Ok(new Response<ComplaintModel?>
            {
                StatusCode = 200,
                Ok = true,
                Data = model,
                Error = null
            });
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> ChangeStatus(string id, [FromBody] StatusRequest status)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(status?.Status))
            {
                return BadRequest(new Response<string?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "ID atau status tidak valid"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.ChangeStatus(id, status.Status);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new Response<string?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = ex.Message
                    }
                });
            }
            catch (Exception ex) when (ex.Message.Contains("Complaint not found"))
            {
                return NotFound(new Response<string?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Komplain tidak ditemukan"
                    }
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

            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = $"Status komplain berhasil diubah menjadi {status.Status}",
                Error = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComplaint(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound(new Response<string?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Komplain tidak ditemukan"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeleteComplaint(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No complaint found"))
            {
                return NotFound(new Response<string?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Komplain tidak ditemukan"
                    }
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

            return Ok(new Response<string?>
            {
                StatusCode = 200,
                Ok = true,
                Data = "Berhasil menghapus komplain",
                Error = null
            });
        }
    }
}