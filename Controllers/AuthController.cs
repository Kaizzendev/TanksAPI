using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanksAPI.Data;
using TanksAPI.DTOs;
using TanksAPI.Models;

namespace TanksAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly GameDbContext _gameDbContext;

    public AuthController(GameDbContext gameDbContext)
    {
        _gameDbContext = gameDbContext;
    }

    [HttpGet("users")]
    public async Task<ActionResult<List<User>>> GetUsers()
    {
        return Ok(await _gameDbContext.Users.ToListAsync());
    }
    
    [HttpGet("users/{id}")]
    public async Task<ActionResult<User>> GetUserById(Guid id)
    {
        var user = await _gameDbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }
    
    [HttpPost("user")]
    public async Task<ActionResult<User>> addUser(User newUser)
    {
        if (newUser == null)
        {
            return BadRequest();
        } 
        
        _gameDbContext.Users.Add(newUser);
        await _gameDbContext.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
    }
    
    [HttpDelete("user/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _gameDbContext.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        
        _gameDbContext.Users.Remove(user);
        await _gameDbContext.SaveChangesAsync();
        return NoContent();
    }
    
    
    [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterRequest request)
        {

            bool exists = await _gameDbContext.Users.AnyAsync(u => u.Username == request.Username);
            
            if (exists)
            {
                return Conflict();
            }

            User user = new()
            {
                Username = request.Username,
                PasswordHash = request.Password
            };
            
            _gameDbContext.Users.Add(user);
            await _gameDbContext.SaveChangesAsync();
            return Created();
        }
        
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginRequest request)
    {
        User? user = await _gameDbContext.Users.FirstOrDefaultAsync(u =>
            u.Username == request.Username);

        if (user == null)
            return Unauthorized();

        if (user.PasswordHash != request.Password)
            return Unauthorized();

        return Ok();
    }
    
}