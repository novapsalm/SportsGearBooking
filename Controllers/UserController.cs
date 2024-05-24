using Backend.Models;
using Backend.Service.IService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
   // [EnableCors("policy")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        // [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<User>>> GetAllUsers()
        {
            var result = await _userService.GetAll();
            if (result != null)
            {
                return Ok(result);
            }
            return NotFound();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<User>> UpdateUser(int id, [FromBody] User user)
        {
            var result = await _userService.UpdateUserDetail(id, user);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost("login/{email}/{password}")]
        public async Task<ActionResult<string>> Login([FromRoute] string email, string password)
        {
            var result = await _userService.Login(email, password);

            if (result != null)
            {
                return Ok(result);
            }
            return Unauthorized();
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUser([FromBody] User user)
        {
            var result = await _userService.AddUserAsync(user);
            if (result != null)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);
            }
            return BadRequest("User already exists. Please login.");
        }
    }
}
