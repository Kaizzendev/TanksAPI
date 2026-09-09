namespace TanksAPI.DTOs;

public class SaveGameRequest
{
    public int CurrentWave { get; set; }
    public int HighestWave { get; set; }
    public int TotalKills { get; set; }
}