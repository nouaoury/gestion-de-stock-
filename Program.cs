using GestionStock.Services;

var builder = WebApplication.CreateBuilder(args);

// Ajouter les services
builder.Services.AddRazorPages();

// Injecter le service de base de données
builder.Services.AddScoped<DatabaseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();