using PendientesPWA.Models.Entities;
using PendientesPWA.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); //para la api
//contexto de la db

//tabla: pendiente
//id, descripcioin , estado
builder.Services.AddRazorPages();
builder.Services.AddSingleton<FokinpendientesContext>();
builder.Services.AddSignalR();
var app = builder.Build();
app.MapControllers();
app.UseStaticFiles(); //Front de la pwa
app.MapRazorPages();
app.MapHub<PendientesHub>("/hub");
app.Run();


//principios solid
//1 Single responsabitiys principie
//2 open/closed principie
//3 liskov sustitution principle
//4 interface sgreggation principle
//5 dependency invertion principle
