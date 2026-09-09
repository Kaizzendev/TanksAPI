using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanksAPI.Data;
using TanksAPI.DTOs;
using TanksAPI.Models;

namespace TanksAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SaveGameController: ControllerBase
{
    private readonly GameDbContext _gameDbContext;

    public SaveGameController(GameDbContext gameDbContext)
    {
        _gameDbContext = gameDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetSaveGame()
    {
        Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        SaveGame? saveGame = await _gameDbContext.SaveGames.FirstOrDefaultAsync(s => s.UserId == userId);

        if (saveGame == null)
        {
            return NotFound();
        }

        SaveGameResponse response = new()
        {
            CurrentWave = saveGame.CurrentWave,
            HighestWave = saveGame.HighestWave,
            TotalKills = saveGame.TotalKills
        };
        return Ok(response);

    }

    [HttpPut]
    public async Task<IActionResult> UpdateSaveGame(SaveGameRequest saveGameRequest)
    {
        Guid userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        SaveGame? saveGame = await _gameDbContext.SaveGames.FirstOrDefaultAsync(s => s.UserId == userId);

        if (saveGame == null)
        {
            return NotFound();
        }

        saveGame.CurrentWave = saveGameRequest.CurrentWave;
        saveGame.HighestWave = saveGameRequest.HighestWave;
        saveGame.TotalKills = saveGameRequest.TotalKills;

        await _gameDbContext.SaveChangesAsync();

        return NoContent();
    }
}