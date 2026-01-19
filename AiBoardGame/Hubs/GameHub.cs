using Microsoft.AspNetCore.SignalR;
using AiBoardGame.Models;
using System.Collections.Concurrent;

namespace AiBoardGame.Hubs;

public class GameHub : Hub
{
    // Lưu danh sách phòng và trạng thái
    private static ConcurrentDictionary<string, List<Player>> _rooms = new();
    private static ConcurrentDictionary<string, string> _playerMap = new();
    private static ConcurrentDictionary<string, bool> _roomStates = new();
    private static ConcurrentDictionary<string, int> _roomTurns = new();

    // QUAN TRỌNG: Lưu số vòng chơi (Round)
    private static ConcurrentDictionary<string, int> _roomRounds = new();

    public async Task CreateRoom(string playerName)
    {
        string roomCode = GenerateRoomCode();
        // Người tạo phòng là Trọng tài (IsObserver = true)
        var host = new Player { ConnectionId = Context.ConnectionId, Name = playerName + " (Trọng tài)", IsHost = true, IsObserver = true, Color = "Grey", Score = 0 };

        _rooms.TryAdd(roomCode, new List<Player> { host });
        _roomStates.TryAdd(roomCode, false);
        _playerMap.TryAdd(Context.ConnectionId, roomCode);
        _roomRounds.TryAdd(roomCode, 1); // Khởi tạo vòng 1

        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        await Clients.Client(Context.ConnectionId).SendAsync("RoomCreated", roomCode, _rooms[roomCode]);
    }

    public async Task JoinRoom(string playerName, string roomCode)
    {
        roomCode = roomCode.ToUpper();
        if (!_rooms.ContainsKey(roomCode)) { await Clients.Caller.SendAsync("JoinError", "Phòng không tồn tại!"); return; }

        var players = _rooms[roomCode];
        if (players.Count(p => !p.IsObserver) >= 4) { await Clients.Caller.SendAsync("JoinError", "Phòng đã đủ 4 người!"); return; }
        if (_roomStates[roomCode]) { await Clients.Caller.SendAsync("JoinError", "Game đang diễn ra!"); return; }

        var newPlayer = new Player
        {
            ConnectionId = Context.ConnectionId,
            Name = playerName,
            IsHost = false,
            IsObserver = false,
            Color = GetColor(players.Count(p => !p.IsObserver)),
            Position = 0
        };

        players.Add(newPlayer);
        _playerMap.TryAdd(Context.ConnectionId, roomCode);
        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        await Clients.Group(roomCode).SendAsync("UpdatePlayers", players, false);
    }

    public async Task StartGame()
    {
        string roomCode = GetRoomCode();
        if (roomCode != null && _rooms.ContainsKey(roomCode))
        {
            var members = _rooms[roomCode];
            // Chỉ chủ phòng mới được bắt đầu
            if (members.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId)?.IsHost == true)
            {
                var firstPlayer = members.FirstOrDefault(p => !p.IsObserver);
                if (firstPlayer != null)
                {
                    _roomStates[roomCode] = true;
                    _roomTurns[roomCode] = members.IndexOf(firstPlayer);
                    _roomRounds[roomCode] = 1; // Reset về vòng 1

                    // QUAN TRỌNG: Gửi về Client cả ID người chơi đầu VÀ số vòng (1)
                    // Nếu thiếu số '1' ở cuối, Home.razor sẽ không chạy!
                    await Clients.Group(roomCode).SendAsync("GameStarted", firstPlayer.ConnectionId, 1);
                }
            }
        }
    }

    public async Task RequestRoll()
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            int turn = _roomTurns[roomCode];
            if (Context.ConnectionId == _rooms[roomCode][turn].ConnectionId)
            {
                int val = new Random().Next(1, 7);
                await Clients.Group(roomCode).SendAsync("DiceRolled", val);
            }
        }
    }

    public async Task EndTurn(List<Player> updatedPlayers)
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            _rooms[roomCode] = updatedPlayers;
            var members = _rooms[roomCode];
            int currentIndex = _roomTurns[roomCode];

            // Tìm người chơi tiếp theo (bỏ qua trọng tài)
            int nextIndex = (currentIndex + 1) % members.Count;
            while (members[nextIndex].IsObserver)
            {
                nextIndex = (nextIndex + 1) % members.Count;
            }

            // Kiểm tra: Nếu quay lại người chơi đầu tiên -> Tăng vòng
            var firstPlayerIndex = members.IndexOf(members.First(p => !p.IsObserver));

            if (nextIndex == firstPlayerIndex)
            {
                _roomRounds[roomCode]++;
            }

            // Kiểm tra Game Over (Sau 10 vòng)
            if (_roomRounds[roomCode] > 10)
            {
                await Clients.Group(roomCode).SendAsync("GameOver", updatedPlayers);
            }
            else
            {
                _roomTurns[roomCode] = nextIndex;
                // Gửi thông tin chuyển lượt + số vòng hiện tại
                await Clients.Group(roomCode).SendAsync("NextTurn", members[nextIndex].ConnectionId, updatedPlayers, _roomRounds[roomCode]);
            }
        }
    }

    // Các hàm hỗ trợ
    private string GetRoomCode() { _playerMap.TryGetValue(Context.ConnectionId, out string? c); return c; }
    private string GenerateRoomCode() => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4).Select(s => s[new Random().Next(s.Length)]).ToArray());
    private string GetColor(int i) => i switch { 0 => "Blue", 1 => "Green", 2 => "Orange", _ => "Red" };
}