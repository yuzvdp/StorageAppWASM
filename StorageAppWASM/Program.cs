using Microsoft.EntityFrameworkCore;
using Microsoft.FluentUI.AspNetCore.Components;
using StorageAppWASM.Components;
using StorageAppWASM.Data;
using StorageAppWASM.Repositories.Abstraction;
using StorageAppWASM.Repositories.Implementation;
using StorageAppWASM.Services.Abstraction;
using StorageAppWASM.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.AddFluentUIComponents();

var connectionString = builder.Configuration.GetConnectionString("Connection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddHttpClient();
builder.Services.AddControllers();
builder.Services.AddScoped<IResourcesRepository, ResourcesRepository>();
builder.Services.AddScoped<IResourcesService, ResourcesService>();

builder.Services.AddScoped<IUnitsRepository, UnitsRepository>();
builder.Services.AddScoped<IUnitsService, UnitsService>();

builder.Services.AddScoped<IBalancesRepository, BalancesRepository>();
builder.Services.AddScoped<IBalancesService, BalancesService>();

builder.Services.AddScoped<IDocumentsIncomeRepository, DocumentsIncomeRepository>();
builder.Services.AddScoped<IDocumentsIncomeService, DocumentsIncomeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.MapControllers();

app.MapStaticAssets();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(StorageAppWASM.Client._Imports).Assembly);


app.Run();
