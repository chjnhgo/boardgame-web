namespace AiBoardGame.Models;

public class QuizQuestion
{
    public string Content { get; set; } = "";
    public List<string> Options { get; set; } = new();
    public int CorrectIndex { get; set; }
    public int Difficulty { get; set; }

    public QuizQuestion() { } // <--- DÒNG NÀY RẤT QUAN TRỌNG
}