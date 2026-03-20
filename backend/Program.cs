var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

using var client = new HttpClient();

app.MapGet("/weather/data", async () =>
{
    var response = await client.GetAsync("https://api.open-meteo.com/v1/forecast?latitude=52.52&longitude=13.41&timezone=Asia%2FBangkok&forecast_days=3");

    if (response.IsSuccessStatusCode)
    {
        return "ยิงสำเร็จ";
    }

    return "ยิงไม่สำเร็จ";
});

app.Run();