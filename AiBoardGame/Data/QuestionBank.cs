using AiBoardGame.Models;

namespace AiBoardGame.Data;

public static class QuestionBank
{
    // Danh sách câu hỏi DỄ (Difficulty = 1) - Ô Xanh Lá
    private static List<QuizQuestion> EasyQuestions = new()
    {
        new() { Content = "Theo Hồ Chí Minh, độc lập dân tộc là:", Options = new(){ "Mục tiêu kinh tế", "Quyền thiêng liêng, bất khả xâm phạm", "Điều kiện hội nhập", "Kết quả kinh tế" }, CorrectIndex = 1, Difficulty = 1 },
        new() { Content = "Hồ Chí Minh khẳng định dân tộc nào cũng có quyền:", Options = new(){ "Phát triển kinh tế", "Thống trị dân tộc khác", "Sống, sung sướng và tự do", "Giàu mạnh hơn" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Mục tiêu trước hết của cách mạng Việt Nam theo Hồ Chí Minh là:", Options = new(){ "Xây dựng CNXH", "Phát triển kinh tế", "Giải phóng dân tộc", "Cải cách ruộng đất" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Theo Hồ Chí Minh, độc lập dân tộc phải gắn liền với:", Options = new(){ "Tăng trưởng kinh tế", "Hội nhập quốc tế", "Tự do, hạnh phúc của nhân dân", "Phát triển văn hóa" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Câu nói 'Nước độc lập mà dân không hưởng hạnh phúc tự do thì độc lập chẳng có nghĩa lý gì' phản ánh quan điểm nào?", Options = new(){ "Độc lập hình thức", "Độc lập gắn liền hạnh phúc nhân dân", "Độc lập kinh tế", "Độc lập văn hóa" }, CorrectIndex = 1, Difficulty = 1 },
        new() { Content = "Cách mạng giải phóng dân tộc muốn thắng lợi phải:", Options = new(){ "Dựa vào viện trợ", "Theo con đường tư sản", "Đi theo con đường cách mạng vô sản", "Cải cách từ từ" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Sự kiện giúp Hồ Chí Minh khẳng định con đường cách mạng vô sản là đúng đắn:", Options = new(){ "Cách mạng Pháp", "Cách mạng Tháng Mười Nga", "Cách mạng Mỹ", "Cách mạng Trung Quốc" }, CorrectIndex = 1, Difficulty = 1 },
        new() { Content = "Luận điểm 'Muốn cứu nước và giải phóng dân tộc không có con đường nào khác con đường cách mạng vô sản' rút ra từ:", Options = new(){ "Thực tiễn VN", "Chủ nghĩa Mác – Lênin", "Phong trào công nhân", "Tất cả các ý trên" }, CorrectIndex = 3, Difficulty = 1 },
        new() { Content = "Lực lượng cách mạng giải phóng dân tộc là:", Options = new(){ "Riêng công nhân", "Riêng nông dân", "Toàn dân tộc", "Trí thức" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Lực lượng lãnh đạo cách mạng Việt Nam theo Hồ Chí Minh là:", Options = new(){ "Mặt trận dân tộc", "Giai cấp nông dân", "Đảng Cộng sản Việt Nam", "Quân đội" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Cách mạng giải phóng dân tộc ở thuộc địa:", Options = new(){ "Phụ thuộc chính quốc", "Không thể thắng", "Có thể thắng lợi trước chính quốc", "Cần viện trợ lớn" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Cách mạng giải phóng dân tộc phải dựa trên:", Options = new(){ "Quân sự thuần túy", "Sự giúp đỡ nước lớn", "Đại đoàn kết toàn dân tộc", "Tư sản dân tộc" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Đại đoàn kết dân tộc theo Hồ Chí Minh dựa trên cơ sở:", Options = new(){ "Kinh tế", "Tôn giáo", "Lòng yêu nước", "Giai cấp" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Cách mạng giải phóng dân tộc phải được tiến hành bằng:", Options = new(){ "Cải cách hòa bình", "Ngoại giao là chính", "Bạo lực cách mạng", "Đấu tranh nghị trường" }, CorrectIndex = 2, Difficulty = 1 },
        new() { Content = "Theo Hồ Chí Minh, bạo lực cách mạng là:", Options = new(){ "Chỉ dùng vũ trang", "Kết hợp chính trị và vũ trang", "Chỉ dùng chính trị", "Chỉ khởi nghĩa" }, CorrectIndex = 1, Difficulty = 1 }
    };

    // Danh sách câu hỏi TẦM TRUNG (Difficulty = 2) - Ô Tím
    private static List<QuizQuestion> MediumQuestions = new()
    {
        new() { Content = "Bản chất sâu xa của chủ nghĩa xã hội là gì?", Options = new(){ "Xóa bỏ tư hữu", "Phát triển sản xuất", "Giải phóng con người", "Công hữu toàn diện" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Hồ Chí Minh tiếp cận chủ nghĩa xã hội chủ yếu từ góc độ nào?", Options = new(){ "Kinh tế", "Chính trị", "Văn hóa, đạo đức, con người", "Lý luận thuần túy" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Mục tiêu trước hết của chủ nghĩa xã hội là gì?", Options = new(){ "Tăng trưởng nhanh", "Công nghiệp hóa", "Nhân dân thoát nghèo, lạc hậu", "Nhà nước pháp quyền" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Điểm giống nhau giữa giai đoạn thấp và cao của CNXH?", Options = new(){ "Phân phối theo lao động", "Lực lượng sản xuất cao", "Không còn bóc lột", "Không còn nhà nước" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "CNXH ở Việt Nam trước hết phải giải quyết vấn đề gì?", Options = new(){ "Xóa kinh tế tư nhân", "Phát triển văn hóa", "Giành độc lập dân tộc", "Xây dựng dân chủ" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Đặc trưng chính trị nổi bật của xã hội XHCN?", Options = new(){ "Nhà nước pháp quyền", "Đảng lãnh đạo", "Nhân dân làm chủ", "Không đấu tranh giai cấp" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Kinh tế của CNXH phải dựa chủ yếu vào đâu?", Options = new(){ "Tư nhân", "Vốn nước ngoài", "Công hữu tư liệu sản xuất chủ yếu", "Thị trường tự do" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Động lực quan trọng nhất của chủ nghĩa xã hội là gì?", Options = new(){ "Khoa học công nghệ", "Lợi nhuận", "Con người và lợi ích nhân dân", "Pháp luật" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Dân chủ trong xã hội XHCN trước hết thể hiện ở đâu?", Options = new(){ "Đời sống văn hóa", "Bầu cử", "Quyền làm chủ của dân", "Tự do kinh doanh" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Quan niệm 'Dân là gốc' thể hiện điều gì?", Options = new(){ "Dân là đối tượng quản lý", "Dân là lực lượng sản xuất", "Dân là chủ thể cách mạng", "Dân cần được giáo dục" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Con đường quá độ lên CNXH ở Việt Nam có đặc điểm gì?", Options = new(){ "Ngắn, thuận lợi", "Giống các nước khác", "Lâu dài, khó khăn, phức tạp", "Chủ yếu hòa bình" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Nhiệm vụ trung tâm của thời kỳ quá độ là gì?", Options = new(){ "Đấu tranh giai cấp", "Cải tạo xã hội cũ", "Phát triển lực lượng sản xuất", "Đối ngoại" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Vì sao VN có thể bỏ qua chế độ tư bản chủ nghĩa?", Options = new(){ "Giúp đỡ quốc tế", "Kinh tế thuận lợi", "Có độc lập và Đảng lãnh đạo", "Dân số đông" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Vai trò của Đảng Cộng sản trong xây dựng CNXH?", Options = new(){ "Đại diện công nhân", "Nắm quyền lực", "Lãnh đạo, tổ chức, định hướng", "Thay thế Nhà nước" }, CorrectIndex = 2, Difficulty = 2 },
        new() { Content = "Xây dựng CNXH trước hết cần xây dựng yếu tố nào?", Options = new(){ "Kinh tế", "Nhà nước", "Con người XHCN", "Vũ trang" }, CorrectIndex = 2, Difficulty = 2 }
    };

    // Danh sách câu hỏi KHÓ (Difficulty = 3) - Ô Vàng
    private static List<QuizQuestion> HardQuestions = new()
    {
        new() { Content = "Vì sao độc lập dân tộc phải gắn liền với chủ nghĩa xã hội?", Options = new(){ "Là mục tiêu cuối cùng", "Chỉ CNXH mới bảo đảm độc lập vững chắc", "Xóa bỏ ngay áp bức", "Độc lập chỉ là phương tiện" }, CorrectIndex = 1, Difficulty = 3 },
        new() { Content = "Luận điểm nào thể hiện tính chất của CNXH ở Việt Nam?", Options = new(){ "Sao chép Liên Xô", "Công nghiệp hóa nhanh", "Giải phóng dân tộc và con người", "Giai đoạn tạm thời" }, CorrectIndex = 2, Difficulty = 3 },
        new() { Content = "Nguyên tắc xây dựng CNXH ở Việt Nam là gì?", Options = new(){ "Ưu tiên kinh tế", "Tuần tự như tư bản", "Phù hợp điều kiện thực tiễn VN", "Xóa sở hữu cá nhân" }, CorrectIndex = 2, Difficulty = 3 },
        new() { Content = "Điểm khác biệt căn bản giữa HCM và mô hình Liên Xô?", Options = new(){ "Vai trò công nghiệp nặng", "Phương pháp đấu tranh", "Sáng tạo, không rập khuôn", "Hình thức sở hữu" }, CorrectIndex = 2, Difficulty = 3 },
        new() { Content = "Động lực chủ yếu của sự nghiệp xây dựng CNXH?", Options = new(){ "Nhà nước", "Đảng Cộng sản", "Quần chúng nhân dân", "Khoa học công nghệ" }, CorrectIndex = 2, Difficulty = 3 },
        new() { Content = "Vai trò của Đảng trong mối quan hệ độc lập dân tộc – CNXH?", Options = new(){ "Lãnh đạo khi chiến tranh", "Nhân tố quyết định định hướng XHCN", "Thay bằng tổ chức khác", "Thứ yếu so với Nhà nước" }, CorrectIndex = 1, Difficulty = 3 },
        new() { Content = "Xây dựng CNXH phải đồng thời thực hiện nhiệm vụ nào?", Options = new(){ "Kinh tế và đối ngoại", "Cải tạo xã hội cũ, xây dựng mới", "Tăng trưởng và bình quân", "Ưu tiên công nghiệp" }, CorrectIndex = 1, Difficulty = 3 },
        new() { Content = "Độc lập dân tộc không phải là mục tiêu cuối cùng vì:", Options = new(){ "Chỉ mang tính hình thức", "Không gắn với dân chủ", "Là tiền đề tiến lên CNXH", "Phải nhường cho hội nhập" }, CorrectIndex = 2, Difficulty = 3 },
        new() { Content = "Nguy cơ làm suy yếu con đường độc lập dân tộc gắn liền CNXH?", Options = new(){ "Tụt hậu kinh tế", "Suy thoái tư tưởng, đạo đức", "Chênh lệch giàu nghèo", "Toàn cầu hóa" }, CorrectIndex = 1, Difficulty = 3 },
        new() { Content = "Sức mạnh của dân chủ XHCN thể hiện ở nguyên tắc nào?", Options = new(){ "Nhà nước quản lý", "Pháp luật trung tâm", "Nhân dân làm chủ", "Kinh tế nhà nước" }, CorrectIndex = 2, Difficulty = 3 }
    };

    public static QuizQuestion GetRandomQuestion(int difficulty)
    {
        var rnd = new Random();
        var list = difficulty switch
        {
            1 => EasyQuestions,
            2 => MediumQuestions,
            3 => HardQuestions,
            _ => EasyQuestions
        };
        return list[rnd.Next(list.Count)];
    }
}