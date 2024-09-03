using PendientesPWA.Models.Entities;
using PendientesPWA.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers(); //para la api
//contexto de la db

//tabla: pendiente
//id, descripcioin , estado
builder.Services.AddSingleton<FokinpendientesContext>();

var app = builder.Build();

app.UseStaticFiles(); //Front de la pwa
app.MapHub<PendientesHub>("/hub");
app.Run();
