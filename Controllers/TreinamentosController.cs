using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using SoldadosDoImperador.Models;

namespace SoldadosDoImperador.Controllers
{
    public class TreinamentosController : Controller
    {
        private readonly ContextoWarhammer _context;

        public TreinamentosController(ContextoWarhammer context)
        {
            _context = context;
        }

        // GET: Treinamentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Treinamentos.ToListAsync());
        }

        // GET: Treinamentos/Details/5
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamentos
                .Include(t => t.Participantes)
                    .ThenInclude(tp => tp.Soldado) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (treinamento == null)
            {
                return NotFound();
            }

            // Lógica M-N para o dropdown
            var idsParticipantes = treinamento.Participantes.Select(p => p.SoldadoId).ToList();
            var soldadosDisponiveis = await _context.Soldados
                .Where(s => !idsParticipantes.Contains(s.Id))
                .OrderBy(s => s.Nome)
                .ToListAsync();

            ViewData["SoldadosDisponiveis"] = new SelectList(soldadosDisponiveis, "Id", "Nome");

            return View(treinamento);
        }

        // GET: Treinamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Treinamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Area,DuracaoHoras,DataRealizacao")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treinamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = treinamento.Id });
            }
            return View(treinamento);
        }

        // GET: Treinamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamentos.FindAsync(id);
            if (treinamento == null)
            {
                return NotFound();
            }
            return View(treinamento);
        }

        // POST: Treinamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Area,DuracaoHoras,DataRealizacao")] Treinamento treinamento)
        {
            if (id != treinamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treinamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreinamentoExists(treinamento.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = treinamento.Id });
            }
            return View(treinamento);
        }

        // GET: Treinamentos/Delete/5
  
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treinamento = await _context.Treinamentos
                .Include(t => t.Participantes)
                    .ThenInclude(tp => tp.Soldado) 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (treinamento == null)
            {
                return NotFound();
            }

            return View(treinamento);
        }

        // POST: Treinamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treinamento = await _context.Treinamentos.FindAsync(id);
            if (treinamento != null)
            {
                var participantes = _context.TreinamentosParticipantes.Where(tp => tp.TreinamentoId == id);
                _context.TreinamentosParticipantes.RemoveRange(participantes);

                _context.Treinamentos.Remove(treinamento);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarParticipante(int treinamentoId, int soldadoId)
        {
            if (soldadoId <= 0)
            {
                return RedirectToAction(nameof(Details), new { id = treinamentoId });
            }

            var jaExiste = await _context.TreinamentosParticipantes
                .AnyAsync(tp => tp.TreinamentoId == treinamentoId && tp.SoldadoId == soldadoId);

            if (!jaExiste)
            {
                var novoParticipante = new TreinamentoParticipante
                {
                    TreinamentoId = treinamentoId,
                    SoldadoId = soldadoId
                };

                _context.TreinamentosParticipantes.Add(novoParticipante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = treinamentoId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverParticipante(int treinamentoId, int soldadoId)
        {
            var participante = await _context.TreinamentosParticipantes
                .FirstOrDefaultAsync(tp => tp.TreinamentoId == treinamentoId && tp.SoldadoId == soldadoId);

            if (participante != null)
            {
                _context.TreinamentosParticipantes.Remove(participante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = treinamentoId });
        }

        private bool TreinamentoExists(int id)
        {
            return _context.Treinamentos.Any(e => e.Id == id);
        }
    }
}