public class QuizQuestion
{
    public int Id { get; set; }
    // Khởi tạo chuỗi rỗng để tránh lỗi Null
    public string Content { get; set; } = string.Empty;
    // Khởi tạo danh sách mới
    public List<string> Options { get; set; } = new List<string>();
    public int CorrectIndex { get; set; }
    public int Difficulty { get; set; }
}