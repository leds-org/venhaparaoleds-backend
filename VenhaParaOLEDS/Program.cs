using VenhaParaOLEDS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Microsoft.Data.SqlClient;
using VenhaParaOLEDS.Services;

// Método utilitário para esperar o banco
static void EsperarSQLServer(string connectionString)
{
    var conectado = false;
    while (!conectado)
    {
        try
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            conectado = true;
            Console.WriteLine("Conexão com o SQL Server estabelecida");
        }
        catch
        {
            Console.WriteLine("Aguardando SQL Server...");
            Thread.Sleep(3000);
        }
    }
}
var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

EsperarSQLServer(connectionString);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString + ";Initial Catalog=VenhaParaOLEDSDb"));

builder.Services.AddScoped<ImportadorService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Definindo porta 8080 para o Kestrel escutar
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(8080);
});

var app = builder.Build();

// Aplica as migrations antes de subir
using (var scope = app.Services.CreateAsyncScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("Aplicando migrations...");
    db.Database.Migrate();
    Console.WriteLine("Migrations aplicadas com successo.");
}


// Importar os dados dos arquivos txt antes de rodar a aplicação
using (var scope = app.Services.CreateScope())
{
    var importador = scope.ServiceProvider.GetRequiredService<ImportadorService>();
    importador.ImportarCandidatos();
    importador.ImportarConcursos();
}

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();