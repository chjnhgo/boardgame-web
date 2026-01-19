namespace AiBoardGame.Services;
using AiBoardGame.Models;

public class AiService
{
    private readonly HttpClient _http;
    private const string ApiKey = "KEY_CUA_BAN"; // Thay bằng Gemini API Key

    public AiService(HttpClient http) => _http = http;

    public async Task<int> SelectQuestionId(Player p, List<QuizQuestion> bank)
    {
        // Gửi thông tin người chơi cho AI để chọn câu hỏi
        // Nếu người chơi có điểm cao (> 5), AI nên chọn câu hỏi khó (Difficulty 3)
        var prompt = $"Người chơi {p.Name} đang có {p.Score} điểm. " +
                     $"Hãy chọn 1 ID từ danh sách câu hỏi: {string.Join(",", bank.Select(q => q.Id))} " +
                     $"để tăng tính kịch tính. Trả về duy nhất con số ID.";

        // Logic gọi API Gemini...
        return bank.First().Id; // Tạm thời trả về ID đầu tiên nếu chưa có API
    }
}