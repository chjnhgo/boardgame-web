using AiBoardGame.Components;
using AiBoardGame.Hubs;
using Microsoft.AspNetCore.ResponseCompression;

var builder = WebApplication.CreateBuilder(args);

// ==================================================================
// 1. ĐĂNG KÝ DỊCH VỤ (SERVICES)
// ==================================================================

// Thêm dịch vụ Razor Components (Blazor Server)
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Thêm dịch vụ SignalR (Quan trọng cho Multiplayer)
builder.Services.AddSignalR();

// Thêm nén phản hồi (Response Compression)
// Giúp giảm độ trễ của SignalR khi deploy lên mạng
builder.Services.AddResponseCompression(opts =>
{
    opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
        new[] { "application/octet-stream" });
});

var app = builder.Build();

// ==================================================================
// 2. CẤU HÌNH PIPELINE (MIDDLEWARE)
// ==================================================================

// Sử dụng nén phản hồi (Đặt trước các middleware khác)
app.UseResponseCompression();

// Cấu hình xử lý lỗi cho môi trường Production
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // Giá trị HSTS mặc định là 30 ngày. Bạn có thể thay đổi nếu cần.
    app.UseHsts();
}

app.UseHttpsRedirection();

// QUAN TRỌNG: Cho phép truy cập thư mục wwwroot (để load ảnh xúc xắc)
app.UseStaticFiles();

// Dùng cho .NET 9 (MapStaticAssets) - Có thể giữ cả hai
app.MapStaticAssets();

// QUAN TRỌNG: Chống giả mạo Request (Bắt buộc phải có để tránh lỗi Runtime)
app.UseAntiforgery();

// ==================================================================
// 3. ĐỊNH TUYẾN (MAPPING)
// ==================================================================

// Ánh xạ giao diện Blazor (UI)
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Ánh xạ SignalR Hub (Server xử lý game)
app.MapHub<GameHub>("/gamehub");

app.Run();