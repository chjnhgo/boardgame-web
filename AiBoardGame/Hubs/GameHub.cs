using Microsoft.AspNetCore.SignalR;
using AiBoardGame.Models;
using System.Collections.Concurrent;

namespace AiBoardGame.Hubs;

public class GameHub : Hub
{
    // Key: Mã phòng, Value: Danh sách tất cả người trong phòng (Host + Players)
    private static ConcurrentDictionary<string, List<Player>> _rooms = new();
    private static ConcurrentDictionary<string, string> _playerMap = new(); // ConnId -> RoomCode
    private static ConcurrentDictionary<string, bool> _roomStates = new(); // RoomCode -> IsStarted
    private static ConcurrentDictionary<string, int> _roomTurns = new(); // RoomCode -> Index của người chơi hiện tại

    // --- 1. NGƯỜI TẠO PHÒNG (HOST - QUAN SÁT VIÊN) ---
    public async Task CreateRoom(string playerName)
    {
        string roomCode = GenerateRoomCode();

        var host = new Player
        {
            ConnectionId = Context.ConnectionId,
            Name = playerName + " (Trọng tài)",
            IsHost = true,
            IsObserver = true, // Host chỉ quan sát
            Color = "Grey",
            Score = 0
        };

        _rooms.TryAdd(roomCode, new List<Player> { host });
        _roomStates.TryAdd(roomCode, false);
        _playerMap.TryAdd(Context.ConnectionId, roomCode);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        await Clients.Client(Context.ConnectionId).SendAsync("RoomCreated", roomCode, _rooms[roomCode]);
    }

    // --- 2. NGƯỜI CHƠI GIA NHẬP (PLAYERS - TỐI ĐA 4) ---
    public async Task JoinRoom(string playerName, string roomCode)
    {
        roomCode = roomCode.ToUpper();

        if (!_rooms.ContainsKey(roomCode))
        {
            await Clients.Caller.SendAsync("JoinError", "Phòng không tồn tại!");
            return;
        }

        var allMembers = _rooms[roomCode];
        // Đếm số lượng người chơi thực sự (không tính Host)
        int playerCount = allMembers.Count(p => !p.IsObserver);

        if (playerCount >= 4)
        {
            await Clients.Caller.SendAsync("JoinError", "Phòng đã đủ 4 người chơi!");
            return;
        }
        if (_roomStates[roomCode])
        {
            await Clients.Caller.SendAsync("JoinError", "Game đang diễn ra!");
            return;
        }

        var newPlayer = new Player
        {
            ConnectionId = Context.ConnectionId,
            Name = playerName,
            IsHost = false,
            IsObserver = false, // Người tham gia là người chơi
            Color = GetColor(playerCount), // Màu dựa trên thứ tự người chơi (0-3)
            Position = 0
        };

        allMembers.Add(newPlayer);
        _playerMap.TryAdd(Context.ConnectionId, roomCode);

        await Groups.AddToGroupAsync(Context.ConnectionId, roomCode);
        await Clients.Group(roomCode).SendAsync("UpdatePlayers", allMembers, false);
    }

    public async Task StartGame()
    {
        string roomCode = GetRoomCode();
        if (roomCode != null && _rooms.ContainsKey(roomCode))
        {
            var members = _rooms[roomCode];
            // Chỉ Host mới được start
            if (members.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId)?.IsHost == true)
            {
                // Tìm người chơi đầu tiên (không phải Observer)
                var firstPlayer = members.FirstOrDefault(p => !p.IsObserver);
                if (firstPlayer != null)
                {
                    _roomStates[roomCode] = true;
                    // _roomTurns lưu index trong danh sách Players thực tế
                    _roomTurns[roomCode] = members.IndexOf(firstPlayer);
                    await Clients.Group(roomCode).SendAsync("GameStarted", firstPlayer.ConnectionId);
                }
            }
        }
    }

    public async Task RequestRoll()
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            int turnIndex = _roomTurns[roomCode];
            var members = _rooms[roomCode];

            // Kiểm tra đúng lượt người chơi
            if (Context.ConnectionId == members[turnIndex].ConnectionId)
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

            // Tìm người chơi kế tiếp (bỏ qua Observer)
            int currentIndex = _roomTurns[roomCode];
            int nextIndex = (currentIndex + 1) % members.Count;

            // Vòng lặp tìm người chơi không phải Observer
            while (members[nextIndex].IsObserver)
            {
                nextIndex = (nextIndex + 1) % members.Count;
            }

            _roomTurns[roomCode] = nextIndex;
            await Clients.Group(roomCode).SendAsync("NextTurn", members[nextIndex].ConnectionId, updatedPlayers);
        }
    }

    private string GetRoomCode()
    {
        _playerMap.TryGetValue(Context.ConnectionId, out string? code);
        return code ?? string.Empty;
    }

    private string GenerateRoomCode()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, 4).Select(s => s[new Random().Next(s.Length)]).ToArray());
    }

    private string GetColor(int i) => i switch { 0 => "Blue", 1 => "Green", 2 => "Orange", _ => "Red" };
}