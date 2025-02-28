using DesafioLeds.Data;
using DesafioLeds.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração para usar o SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data source=projeto.db")
);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Seed inicial dos dados
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated(); // Cria o banco se não existir

    if (!context.Candidatos.Any())
    {
        // Adicionar candidatos de exemplo
        context.Candidatos.AddRange(
            new Candidato
            {
                Nome = "Lindsey Craft",
                DataNascimento = "19/05/1976",
                CPF = "182.845.084-34",
                Profissoes = new List<string> { "carpinteiro" }
            },
            new Candidato
            {
                Nome = "Jackie Dawson",
                DataNascimento = "14/08/1970",
                CPF = "311.667.973-47",
                Profissoes = new List<string> { "marceneiro", "assistente administrativo" }
            },
            new Candidato
            {
                Nome = "Cory Mendoza",
                DataNascimento = "11/02/1957",
                CPF = "565.512.353-92",
                Profissoes = new List<string> { "carpinteiro", "marceneiro" }
            }
        );

        // Adicionar concursos de exemplo
        context.Concursos.AddRange(
            new Concurso
            {
                Orgao = "SEDU",
                Edital = "9/2016",
                Codigo = "61828450843",
                Vagas = new List<string> { "analista de sistemas", "marceneiro" }
            },
            new Concurso
            {
                Orgao = "SEJUS",
                Edital = "15/2017",
                Codigo = "61828450843",
                Vagas = new List<string> { "carpinteiro", "professor de matemática", "assistente administrativo" }
            }, 
            new Concurso
            {
                Orgao = "SEJUS",
                Edital = "17/2017",
                Codigo = "95655123539",
                Vagas = new List<string> {"professor de matemática"}
            }
        );

        context.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
