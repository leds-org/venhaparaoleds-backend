using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DesafioLeds.Models;
using DesafioLeds.Data;
using Microsoft.EntityFrameworkCore;

namespace DesafioLeds.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;
    //private readonly ILogger<HomeController> _logger;

    //public HomeController(ILogger<HomeController> logger)
    //{
    //    _logger = logger;
    //}

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> BuscarConcursos(string cpf)
    {
        var candidato = await _context.Candidatos
            .FirstOrDefaultAsync(c => c.CPF == cpf);

        if (candidato == null)
        {
            ViewBag.Mensagem = "CPF não encontrado!";
            return View("Index");
        }

        // Carrega todos os concursos do banco de dados
        var todosConcursos = await _context.Concursos.ToListAsync();

        // Filtra os concursos em memória
        var concursos = todosConcursos
            .Where(c => c.Vagas.Any(v => candidato.Profissoes.Contains(v)))
            .ToList();

        return View("BuscarConcursos", concursos);
    }

    [HttpPost]
    public async Task<IActionResult> BuscarCandidatos(string codigoConcurso)
    {
        var concurso = await _context.Concursos
            .FirstOrDefaultAsync(c => c.Codigo == codigoConcurso);

        if (concurso == null)
        {
            ViewBag.Mensagem = "Código do concurso não encontrado!";
            return View("Index");
        }

        // Carrega todos os candidatos do banco de dados
        var todosCandidatos = await _context.Candidatos.ToListAsync();

        // Filtra os candidatos em memória
        var candidatos = todosCandidatos
            .Where(c => c.Profissoes.Any(p => concurso.Vagas.Contains(p)))
            .ToList();

        return View("BuscarCandidatos", candidatos);
    }
}
