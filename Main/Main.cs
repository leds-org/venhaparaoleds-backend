using Microsoft.EntityFrameworkCore;
using LedsAplication.Entidades;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<DbContext>()
            .UseMySql("server=localhost;database=ledsdb;user=root;password=Banbanana99!",
                new MySqlServerVersion(new Version(8, 0, 29))) // use o seu Pomelo
            .Options;
        using var context = new DbContext(options);
        var leitorCandidatos = new LeituraService(context);
        var leitorConcursos = new Leitura_concursos(context);
        var casamento = new Casamento(context);

        string caminhoCandidatos = C:\Users\grong\OneDrive\Anexos\Arquivos_do_desafio\candidatos.txt;
        string caminhoConcursos = C:\Users\grong\OneDrive\Anexos\Arquivos_do_desafio\concursos.txt;

        Console.WriteLine("📥 Calmai! :D importando candidatos...");
        await leitorCandidatos.ImportarArquivos_CandidatosAsync(caminhoCandidatos);

        Console.WriteLine("📥 Calmai! :D importando concursos...");
        await leitorConcursos.ImportarArquivos_Concursos(caminhoConcursos);

        Console.WriteLine("🔎 Buscando relações entre candidatos e concursos...");
        var resultados = await casamento.ObterConcursosCompativeisAsync();

        foreach (var (candidato, concursos) in resultados)
        {
            Console.WriteLine($"\n👤 Candidato: {candidato.Nome} ({candidato.CPF})");
            if (!concursos.Any())
            {
                Console.WriteLine("❌ Nenhum concurso compatível encontrado.");
            }
            else
            {
                Console.WriteLine("✅ Concursos compatíveis:");
                foreach (var concurso in concursos)
                {
                    Console.WriteLine($"  • {concurso.orgao} - Edital: {concurso.Edital.ToShortDateString()} - Código: {concurso.CdConcurso}");
                }
            }
        }
    }
}