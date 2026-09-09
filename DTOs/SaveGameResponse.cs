namespace TanksAPI.DTOs;

public class SaveGameResponse
{
    public int CurrentWave { get; set; }
    public int HighestWave { get; set; }
    public int TotalKills { get; set; }
}