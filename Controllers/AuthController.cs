using Microsoft.AspNetCore.Mvc;
using TanksAPI.Models;

namespace TanksAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private static readonly List<User> Users =
    [
        new User
        {
            ID = "1",
            name = "Geralt",
            password = "Kaer"
        },
        new User 
        {
            ID = "2",
            name = "Ciri",
            password = "Pajarillo"
        }
    ];

    [HttpGet("users")]
    public ActionResult<List<User>> GetUsers()
    {
        return Ok(Users);
    }

    [HttpGet("users/{id}")]
    public ActionResult<User> GetUserById(string id)
    {
        var user = Users.FirstOrDefault(x => x.ID == id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    [HttpPost("user")]
    public ActionResult<User> addUser(User newUser)
    {
        if (newUser == null)
        {
            return BadRequest();
        } 
        
        Users.Add(newUser);
        return CreatedAtAction(nameof(GetUserById), new { id = newUser.ID }, newUser);
    }

    [HttpDelete("user/{id}")]
    public ActionResult DeleteUser(string id)
    {
        var user = Users.FirstOrDefault(x => x.ID == id);
        if (user == null)
        {
            return NotFound();
        }
        
        Users.Remove(user);
        return NoContent();
    }
}