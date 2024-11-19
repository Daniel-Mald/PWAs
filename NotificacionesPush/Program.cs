using NotificacionesPush.Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddTransient<NotificationService>();
builder.Services.AddControllers();
var app = builder.Build();


app.MapControllers();
app.MapRazorPages();
app.UseStaticFiles();

app.Run();
