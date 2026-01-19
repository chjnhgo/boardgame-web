using AiBoardGame.Models;
namespace AiBoardGame.Data;

public static class QuestionBank
{
    public static List<QuizQuestion> Questions = new() {
        new QuizQuestion { Id = 1, Content = "Bác Hồ ra đi tìm đường cứu nước tại bến cảng nào?", Options = new() {"Hải Phòng", "Bến Nhà Rồng", "Đà Nẵng", "Ba Son"}, CorrectIndex = 1, Difficulty = 1 },
        new QuizQuestion { Id = 2, Content = "Bác Hồ đọc Bản Tuyên ngôn Độc lập vào ngày nào?", Options = new() {"19/5/1890", "3/2/1930", "2/9/1945", "19/12/1946"}, CorrectIndex = 2, Difficulty = 1 },
        new QuizQuestion { Id = 3, Content = "Tác phẩm nào sau đây của Bác được xuất bản năm 1927?", Options = new() {"Bản án chế độ thực dân Pháp", "Đường Kách mệnh", "Nhật ký trong tù", "Lời kêu gọi toàn quốc kháng chiến"}, CorrectIndex = 1, Difficulty = 3 },
        new QuizQuestion { Id = 4, Content = "Trong Di chúc, Bác căn dặn điều đầu tiên là về vấn đề gì?", Options = new() {"Kinh tế", "Thanh niên", "Trong Đảng", "Phụ nữ"}, CorrectIndex = 2, Difficulty = 2 }
    };
}