using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CURD_API_LEARING.Models;
using CURD_API_LEARING.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CURD_API_LEARING.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserContext _dbContext;

        public UserController(UserContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext), "Database context cannot be null.");
        }

        // GET: api/User
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_dbContext.Users == null)
            {
                return NotFound("Users entity set not found.");
            }

            var users = await _dbContext.Users.ToListAsync();
            if (users == null || users.Count == 0)
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        // GET: api/User/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound("Users entity set not found.");
            }

            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            if (_dbContext.Users == null)
            {
                return NotFound("Users entity set not found.");
            }

            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (_dbContext.Users == null)
            {
                return NotFound("Users entity set not found.");
            }

            if (id != user.ID)
            {
                return BadRequest("User ID mismatch.");
            }

            if (user == null)
            {
                return BadRequest("Invalid user data.");
            }

            _dbContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound($"User with ID {id} not found.");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/User/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (_dbContext.Users == null)
            {
                return NotFound("Users entity set not found.");
            }

            var user = await _dbContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            _dbContext.Users.Remove(user);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return _dbContext.Users.Any(e => e.ID == id);
        }
    }
}
