using System.Collections.Generic;

namespace AiBoardGame.Models;

public class QuizQuestion
{
    // --- THÊM DÒNG NÀY ĐỂ SỬA LỖI CS1061 ---
    public int Id { get; set; }

    public string Content { get; set; } = "";
    public List<string> Options { get; set; } = new();
    public int CorrectIndex { get; set; }
    public int Difficulty { get; set; }

    // Constructor rỗng (Bắt buộc cho SignalR)
    public QuizQuestion() { }
}