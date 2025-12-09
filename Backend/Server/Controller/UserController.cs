using Microsoft.AspNetCore.Mvc;
using User;
using Server.Response;

namespace Server.Controller
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string _connString;

        public UserController(IConfiguration configuration)
        {
            _connString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException("Connection string 'DefaultConnection' not found.");
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                Service service = new(_connString);
                var users = await service.GetUsers();
                return Ok(new Response<List<Model>?>
                {
                    StatusCode = 200,
                    Ok = true,
                    Data = users,
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
        public async Task<IActionResult> GetUserById(int id)
        {
            Model? user = null;
            if (id <= 0)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "User not found"
                    }
                });
            }
            Service service = new(_connString);
            try
            {
                user = await service.GetUserById(id);
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
            if (user == null)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "User not found"
                    }
                });
            }
            return Ok(new Response<Model?>
            {
                StatusCode = 200,
                Ok = true,
                Data = user,
                Error = null
            });

        }


        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] Model model)
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
                        message = "Invalid user data"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                var id = await service.CreateUser(model);
                model.UserId = id;
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
                        message = "User with the same ID already exists"
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
        public async Task<IActionResult> UpdateUser(int id, [FromBody] Model model)
        {

            if (model == null || id <= 0)
            {
                return BadRequest(new Response<Model?>
                {
                    StatusCode = 400,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "Invalid user data or ID"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.UpdateUser(id, model);
                model.UserId = id;
            }
            catch (Exception ex) when (ex.Message.Contains("No user found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "User not found"
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
        public async Task<IActionResult> DeleteUser(int id)
        {

            if (id <= 0)
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new
                    {
                        message = "User not found"
                    }
                });
            }

            Service service = new(_connString);
            try
            {
                await service.DeleteUser(id);
            }
            catch (Exception ex) when (ex.Message.Contains("No user found"))
            {
                return NotFound(new Response<Model?>
                {
                    StatusCode = 404,
                    Ok = false,
                    Data = null,
                    Error = new 
                    {
                        message = "User not found"
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
                Data = "Delete successful",
                Error = null
            });

        }
    }
}
