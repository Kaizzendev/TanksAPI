namespace TanksAPI.DTOs;

public class LeaderboardResponse
{
    public string Username { get; set; } = string.Empty;
    public int HighestWave { get; set; }
    public int TotalKills { get; set; }
}