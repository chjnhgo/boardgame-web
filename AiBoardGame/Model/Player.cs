namespace AiBoardGame.Models;

public class Player
{
    public string ConnectionId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Color { get; set; } = string.Empty;
    public int Position { get; set; } = 0;
    public int Score { get; set; } = 0;
    public bool IsHost { get; set; } = false;
    public bool IsObserver { get; set; } = false; // Thuộc tính mới: True = Chỉ xem
}