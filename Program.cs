using ElectronNET.API;
using ElectronNET.API.Entities;

/* 
    Tworzenie instancji klasy odpowiedzialnej
    za inicjalizacje naszej aplikacji.
*/
var builder = WebApplication.CreateBuilder(args);

// Dodawanie Electrona do projektu.
builder.WebHost.UseElectron(args);

// Dodawanie komponentów Razora, Electrona do backendu naszej aplikacji.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddElectron();

// Inicjalizacja naszej aplikacji wraz z dodanymi komponentami.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
}

/*
    Dodawanie plików statycznych. Czyli np. obrazków.
    Pliki te znajdują się w folderze wwwroot
*/
app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<ProjektZaliczeniowy_AiSD2.Components.App>()
    .AddInteractiveServerRenderMode();


// Uruchamiania backendu ASP.NET
await app.StartAsync();

// Tworzenie nowego okna Electron.
var window = await Electron.WindowManager.CreateWindowAsync();

window.SetContentSize(1280, 720);
window.SetMenuBarVisibility(false);
window.SetTitle("Projekt zaliczeniowy AiSD 2");

// Oczekiwanie na zakończenie okna Electron.
app.WaitForShutdown();
