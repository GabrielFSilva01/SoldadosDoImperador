using Microsoft.AspNetCore.Mvc;
using SoldadosDoImperador.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using SoldadosDoImperador.Models; 
using SoldadosDoImperador.ViewModels; 
using System.Collections.Generic; 

namespace SoldadosDoImperador.Controllers
{
    
    [Authorize(Roles = "PRIMARCH")]
    public class ConsultaController(ContextoWarhammer context) : Controller
    {
        private readonly ContextoWarhammer _context = context;

      
        public IActionResult Index()
        {
            return View();
        }

       
        [HttpGet]
        public IActionResult PesquisaFiltro()
        {
            ViewData["Patentes"] = new SelectList(Enum.GetValues<Patente>());
            ViewData["Missoes"] = new SelectList(_context.Missoes.OrderBy(m => m.Nome), "Id", "Nome");
            return View();
        }

        // GET: /Consulta/ResultadoFiltro
        [HttpGet]
        public async Task<IActionResult> ResultadoFiltro(string? filtroNome, Patente? filtroPatente, int? filtroMissaoId)
        {
            var query = _context.Soldados.AsQueryable();

            if (!string.IsNullOrEmpty(filtroNome))
            {
                query = query.Where(s => s.Nome.Contains(filtroNome));
                ViewData["FiltroNomeAtual"] = filtroNome;
            }
            if (filtroPatente.HasValue)
            {
                query = query.Where(s => s.Patente == filtroPatente.Value);
                ViewData["FiltroPatenteAtual"] = filtroPatente.Value;
            }
            if (filtroMissaoId.HasValue)
            {
                query = query.Where(s => s.MissoesParticipadas.Any(mp => mp.MissaoId == filtroMissaoId.Value));
                ViewData["FiltroMissaoAtual"] = _context.Missoes.Find(filtroMissaoId.Value)?.Nome;
            }

            var listaFiltrada = await query.ToListAsync();
            return View(listaFiltrada);
        }

      
        [HttpGet]
        public async Task<IActionResult> ListarSoldadosParaPerfil()
        {
           
            var soldados = await _context.Soldados
                                    .OrderBy(s => s.Nome)
                                   
                                    .Select(s => new {
                                        s.Id,
                                        s.Nome,
                                        s.Patente,
                                        s.Capitulo 
                                    })
                                    .ToListAsync();
            return View(soldados);
        }

        
        [HttpGet]
        public async Task<IActionResult> PerfilCompleto(int? id)
        {
            if (id == null) return NotFound();
            var soldado = await _context.Soldados
                .Include(s => s.MissoesParticipadas).ThenInclude(mp => mp.Missao)
                .Include(s => s.TreinamentosParticipados).ThenInclude(tp => tp.Treinamento)
                .Include(s => s.ItensDeBatalha)
                .Include(s => s.OrdensRecebidas)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soldado == null) return NotFound();
            return View(soldado);
        }

      
        [HttpGet]
        public async Task<IActionResult> AgrupamentoCapitulo()
        {
            var consulta = _context.Soldados
                .GroupBy(s => s.Capitulo, (capitulo, soldados) => new
                {
                    NomeDoGrupo = capitulo.ToString(),
                    Contagem = soldados.Count()
                })
                .OrderByDescending(x => x.Contagem);
            var resultado = await consulta.ToListAsync();
            return View(resultado);
        }

   
        [HttpGet]
        public async Task<IActionResult> PivoteamentoCapituloPatente()
        {
            var dadosAgrupados = await _context.Soldados
                .GroupBy(s => new { s.Capitulo, s.Patente }, (key, soldados) => new
                {
                    Capitulo = key.Capitulo.ToString(),
                    Patente = key.Patente.ToString(),
                    Contagem = soldados.Count()
                })
                .ToListAsync();

            var viewModel = new PivoteamentoViewModel();
            viewModel.CabecalhosPatentes = Enum.GetNames(typeof(Patente)).ToList();
            var gruposCapitulo = dadosAgrupados.GroupBy(d => d.Capitulo);

            foreach (var grupo in gruposCapitulo)
            {
                var linha = new LinhaPivotCapitulo { NomeCapitulo = grupo.Key };
                foreach (var patente in viewModel.CabecalhosPatentes)
                {
                    var item = grupo.FirstOrDefault(g => g.Patente == patente);
                    int contagem = item?.Contagem ?? 0;
                    linha.ContagemPorPatente.Add(patente, contagem);
                }
                viewModel.LinhasCapitulo.Add(linha);
            }
            viewModel.LinhasCapitulo = viewModel.LinhasCapitulo.OrderBy(l => l.NomeCapitulo).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> AgrupadoPorPatente()
        {
            var consulta = from s in _context.Soldados
                           let nomeCapitulo = s.Capitulo.ToString()
                           orderby s.Patente, s.Nome
                           select new AgrupPorPatente
                           {
                               id = s.Id,
                               nome = s.Nome,
                               Patente = s.Patente.ToString(),
                               Capitulo = nomeCapitulo
                           };
            var listaAgrupada = await consulta.ToListAsync();
            return View(listaAgrupada);
        }

    }
} 