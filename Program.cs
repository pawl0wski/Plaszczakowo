using Blazored.Modal;
using BlazorTransitionableRoute;
using ElectronNET.API;
using ProjektZaliczeniowy_AiSD2.States;
using App = ProjektZaliczeniowy_AiSD2.Components.App;

/*
    Tworzenie instancji klasy odpowiedzialnej
    za inicjalizacje naszej aplikacji.
*/
var builder = WebApplication.CreateBuilder(args);


// Dodawanie komponentów Razora, Electrona do backendu naszej aplikacji.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddElectron();

// Dodawanie ProblemState który przechowuje wejście problemu między stronami
builder.Services.AddScoped<IProblemState, ProblemState>();

builder.Services.AddSingleton<IFenceState, FenceState>();

// Dodawanie Electrona do projektu.
builder.WebHost.UseElectron(args);

builder.Services.AddBlazoredModal();

builder.Services.AddScoped<IRouteTransitionInvoker, DefaultRouteTransitionInvoker>();

// Inicjalizacja naszej aplikacji wraz z dodanymi komponentami.
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) app.UseExceptionHandler("/Error", true);

/*
    Dodawanie plików statycznych. Czyli np. obrazków.
    Pliki te znajdują się w folderze wwwroot
*/
app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Uruchamiania backendu ASP.NET
await app.StartAsync();

// Tworzenie nowego okna Electron.
var window = await Electron.WindowManager.CreateWindowAsync();

// Ustawianie wielkości okna
window.SetContentSize(1280, 720);
// Ukrywanie górnego menu
window.SetMenuBarVisibility(false);
// Ustawienie tytułu okna
window.SetTitle("Płaszczakowo");

// Stworzenie testowych .json dla podproblemów
DefaultProblemInputProvider.CreateDefaultFilesForProblems();

// Oczekiwanie na zakończenie okna Electron.
app.WaitForShutdown();