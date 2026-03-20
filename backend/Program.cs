DotNetEnv.Env.Load();

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapGet("/weather/data", async () =>
{
    // 1. สร้าง Browser จำลองขึ้นมา ใช้ using เพื่อให้มันทำลายตัวเองทิ้งเมื่อใช้เสร็จ จะได้ไม่กินเมมโมรี่
    using var client = new HttpClient();
    
    string api = Environment.GetEnvironmentVariable("METRO_WEATHER_API");

    // 2. สั่งให้ไปดึงข้อมูลจาก URL (ใช้ await เพราะต้องรอข้อมูลวิ่งไป-กลับผ่านอินเทอร์เน็ต)
    var response = await client.GetAsync(api);

    // ถ้ายิงสำเร็จ response.IsSuccessStatusCode จะมีค่าเป็น true
    if (response.IsSuccessStatusCode)
    {
        return await response.Content.ReadAsStringAsync();
    }

    return "ยิงไม่สำเร็จ";
});

app.Run();