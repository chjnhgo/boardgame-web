using Microsoft.AspNetCore.SignalR;
using AiBoardGame.Models;
using System.Collections.Concurrent;

namespace AiBoardGame.Hubs;

public class GameHub : Hub
{
    private static ConcurrentDictionary<string, List<Player>> _rooms = new();
    private static ConcurrentDictionary<string, string> _playerMap = new();
    private static ConcurrentDictionary<string, bool> _roomStates = new();
    private static ConcurrentDictionary<string, int> _roomTurns = new();
    private static ConcurrentDictionary<string, int> _roomRounds = new();

    // --- CÁC HÀM CƠ BẢN (GIỮ NGUYÊN) ---
    public async Task CreateRoom(string playerName)
    {
        string roomCode = GenerateRoomCode();
        var host = new Player { ConnectionId = Context.ConnectionId, Name = playerName + " (Trọng tài)", IsHost = true, IsObserver = true, Color = "Grey", Score = 0 };
        _rooms.TryAdd(roomCode, new List<Player> { host });
        _roomStates.TryAdd(roomCode, false);
        _playerMap.TryAdd(Context.ConnectionId, roomCode);
        _roomRounds.TryAdd(roomCode, 1);
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

        var newPlayer = new Player { ConnectionId = Context.ConnectionId, Name = playerName, IsHost = false, IsObserver = false, Color = GetColor(players.Count(p => !p.IsObserver)), Position = 0 };
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
            if (members.FirstOrDefault(p => p.ConnectionId == Context.ConnectionId)?.IsHost == true)
            {
                var firstPlayer = members.FirstOrDefault(p => !p.IsObserver);
                if (firstPlayer != null)
                {
                    _roomStates[roomCode] = true;
                    _roomTurns[roomCode] = members.IndexOf(firstPlayer);
                    _roomRounds[roomCode] = 1;
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

    // --- CÁC HÀM MỚI ĐỂ ĐỒNG BỘ MÀN HÌNH (SYNC) ---

    // 1. Đồng bộ hiển thị câu hỏi cho mọi người cùng xem
    public async Task SyncQuizStart(QuizQuestion question)
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            await Clients.Group(roomCode).SendAsync("ShowQuiz", question, Context.ConnectionId);
        }
    }

    // 2. Đồng bộ hiển thị màn hình chọn mục tiêu tấn công
    public async Task SyncAttackStart()
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            await Clients.Group(roomCode).SendAsync("ShowAttack", Context.ConnectionId);
        }
    }

    // 3. Đồng bộ kết quả trả lời/tấn công và đóng Popup
    public async Task SyncResult(string message, List<Player> updatedPlayers)
    {
        // Hàm này vừa thông báo kết quả, vừa cập nhật điểm số mới nhất, vừa EndTurn luôn
        await EndTurnLogic(updatedPlayers, message);
    }

    // Logic chuyển lượt (Tách ra để dùng chung)
    private async Task EndTurnLogic(List<Player> updatedPlayers, string popupMsg)
    {
        string roomCode = GetRoomCode();
        if (roomCode != null)
        {
            _rooms[roomCode] = updatedPlayers;
            var members = _rooms[roomCode];
            int currentIndex = _roomTurns[roomCode];

            int nextIndex = (currentIndex + 1) % members.Count;
            while (members[nextIndex].IsObserver) nextIndex = (nextIndex + 1) % members.Count;

            var firstPlayerIndex = members.IndexOf(members.First(p => !p.IsObserver));
            if (nextIndex == firstPlayerIndex) _roomRounds[roomCode]++;

            if (_roomRounds[roomCode] > 10)
            {
                await Clients.Group(roomCode).SendAsync("GameOver", updatedPlayers);
            }
            else
            {
                _roomTurns[roomCode] = nextIndex;
                // Gửi sự kiện: Đóng Popup -> Hiện thông báo kết quả -> Chuyển lượt
                await Clients.Group(roomCode).SendAsync("TurnResultAndNext", popupMsg, members[nextIndex].ConnectionId, updatedPlayers, _roomRounds[roomCode]);
            }
        }
    }

    // Chỉ dùng để cập nhật dữ liệu nếu không có popup (VD: đi vào ô lùi bước)
    public async Task EndTurn(List<Player> updatedPlayers)
    {
        await EndTurnLogic(updatedPlayers, null); // null msg nghĩa là không cần popup thông báo kết quả đặc biệt
    }

    private string GetRoomCode() { _playerMap.TryGetValue(Context.ConnectionId, out string? c); return c; }
    private string GenerateRoomCode() => new string(Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 4).Select(s => s[new Random().Next(s.Length)]).ToArray());
    private string GetColor(int i) => i switch { 0 => "Blue", 1 => "Green", 2 => "Orange", _ => "Red" };
}