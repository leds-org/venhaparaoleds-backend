using VenhaParaOLEDS.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Writers;
using Microsoft.Data.SqlClient;
using VenhaParaOLEDS.Services;
using Microsoft.AspNetCore.Mvc.Routing;

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
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API - VenhParaOLDES",
        Version = "v1",
        Description = "API para gerenciamento de concursos e candidatos.",
        Contact = new OpenApiContact
        {
            Name = "Leonarda Amaral",
            Email = "leonardajobs@gmail.com",
            Url = new Uri("https://github.com/le-amaral")
        },
        License = new OpenApiLicense
        {
            Name = "MIT",
            Url = new Uri("https://opensource.org/licenses/MIT")
        }
    });
    // Caminho do arquivo XML gerado na buil (com os comentários do código)
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

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