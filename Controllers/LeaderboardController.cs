using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TanksAPI.Data;
using TanksAPI.DTOs;
using TanksAPI.Models;

namespace TanksAPI.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LeaderboardController: ControllerBase
{
    private readonly GameDbContext _gameDbContext;

    public LeaderboardController(GameDbContext gameDbContext)
    {
        _gameDbContext = gameDbContext;
    }


    [HttpGet]
    public async Task<IActionResult> GetLeaderboard()
    {
        var leaderboard = await _gameDbContext.SaveGames
            .Include(s => s.User)
            .OrderByDescending(s => s.HighestWave)
            .Take(50)
            .Select(s => new LeaderboardResponse
            {
                Username = s.User.Username,
                HighestWave = s.HighestWave,
                TotalKills = s.TotalKills
            })
            .ToListAsync();
        
        return Ok(leaderboard);
    }
}