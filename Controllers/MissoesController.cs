using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using SoldadosDoImperador.Models;
using Microsoft.AspNetCore.Authorization; 

namespace SoldadosDoImperador.Controllers
{
    [Authorize] 
    public class MissoesController(ContextoWarhammer context) : Controller
    {
        private readonly ContextoWarhammer _context = context;

      

        // GET: Missoes 
        public async Task<IActionResult> Index()
        {
            return View(await _context.Missoes.ToListAsync());
        }

        // GET: Missoes/Details/5 (Restrito ao PRIMARCH)
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missao = await _context.Missoes
                .Include(m => m.Participantes)
                    .ThenInclude(mp => mp.Soldado)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (missao == null)
            {
                return NotFound();
            }

            var idsParticipantes = missao.Participantes.Select(p => p.SoldadoId).ToList();
            var soldadosDisponiveis = await _context.Soldados
                .Where(s => !idsParticipantes.Contains(s.Id))
                .OrderBy(s => s.Nome)
                .ToListAsync();

            ViewData["SoldadosDisponiveis"] = new SelectList(soldadosDisponiveis, "Id", "Nome");

            return View(missao);
        }

        // GET: Missoes/Create
        [Authorize(Roles = "PRIMARCH")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Missoes/Create
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Objetivo,Status,DataInicio,Localizacao,DataFim")] Missao missao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { id = missao.Id });
            }
            return View(missao);
        }

        // GET: Missoes/Edit/5
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missao = await _context.Missoes.FindAsync(id);
            if (missao == null)
            {
                return NotFound();
            }
            return View(missao);
        }

        // POST: Missoes/Edit/5
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Objetivo,Status,DataInicio,Localizacao,DataFim")] Missao missao)
        {
            if (id != missao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(missao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissaoExists(missao.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Details), new { id = missao.Id });
            }
            return View(missao);
        }

        // GET: Missoes/Delete/5
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missao = await _context.Missoes
                .Include(m => m.Participantes)
                    .ThenInclude(mp => mp.Soldado)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (missao == null)
            {
                return NotFound();
            }

            return View(missao);
        }

        // POST: Missoes/Delete/5
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var missao = await _context.Missoes.FindAsync(id);
            if (missao != null)
            {
                var participantes = _context.MissoesParticipantes.Where(mp => mp.MissaoId == id);
                _context.MissoesParticipantes.RemoveRange(participantes);

                _context.Missoes.Remove(missao);

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Missoes/AdicionarParticipante
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdicionarParticipante(int missaoId, int soldadoId)
        {
            if (soldadoId <= 0)
            {
                return RedirectToAction(nameof(Details), new { id = missaoId });
            }

            var jaExiste = await _context.MissoesParticipantes
                .AnyAsync(mp => mp.MissaoId == missaoId && mp.SoldadoId == soldadoId);

            if (!jaExiste)
            {
                var novoParticipante = new MissaoParticipante
                {
                    MissaoId = missaoId,
                    SoldadoId = soldadoId
                };

                _context.MissoesParticipantes.Add(novoParticipante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = missaoId });
        }

        // POST: Missoes/RemoverParticipante
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoverParticipante(int missaoId, int soldadoId)
        {
            var participante = await _context.MissoesParticipantes
                .FirstOrDefaultAsync(mp => mp.MissaoId == missaoId && mp.SoldadoId == soldadoId);

            if (participante != null)
            {
                _context.MissoesParticipantes.Remove(participante);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Details), new { id = missaoId });
        }

        private bool MissaoExists(int id)
        {
            return _context.Missoes.Any(e => e.Id == id);
        }
    }
}