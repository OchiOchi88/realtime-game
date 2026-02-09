using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.OpenApi.Models;
using Server.StreamingHubs;

var builder = WebApplication.CreateBuilder(args);

// --- 1. Kestrel の設定 (Caddyとの通信に必須) ---
builder.WebHost.ConfigureKestrel(options =>
{
    // 8080ポートで待受
    options.ListenAnyIP(8080, listenOptions =>
    {
        // 重要: HTTP/2 専用に固定します。
        // これにより Caddy からの h2c (暗号化なしHTTP/2) を正しく受け入れます。
        listenOptions.Protocols = HttpProtocols.Http2;
    });
});
// --- 2. サービス登録 ---
builder.Services.AddSingleton<RoomContextRepository>();

// MagicOnion の登録
builder.Services.AddMagicOnion();

// CORS設定
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Grpc-Status", "Grpc-Message", "Grpc-Encoding", "Grpc-Accept-Encoding");
    });
});

var app = builder.Build();
// --- 3. パイプライン設定 (順番が重要) ---
app.UseRouting();
app.UseCors("AllowAll");

// MagicOnion サービスのマッピング
app.MapMagicOnionService();

// 疎通確認用のルート
app.MapGet("/", () => "MagicOnion Server is running (HTTP/2)");

app.Run();
