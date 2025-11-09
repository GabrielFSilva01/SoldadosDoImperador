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
    public class OrdensController(ContextoWarhammer context) : Controller
    {
        private readonly ContextoWarhammer _context = context;

       

        private IQueryable<Ordem> ObterOrdensComRelacoes()
        {
            return _context.Ordens.Include(o => o.Soldado);
        }

        // GET: Ordens 
        public async Task<IActionResult> Index()
        {
            return View(await ObterOrdensComRelacoes().ToListAsync());
        }

        // GET: Ordens/Details/5 
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await ObterOrdensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // GET: Ordens/Create
        [Authorize(Roles = "PRIMARCH")]
        public IActionResult Create()
        {
            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome");
            ViewData["Status"] = new SelectList(Enum.GetValues<StatusOrdem>());
            return View();
        }

        // POST: Ordens/Create
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Titulo,Descricao,Status,DataEmissao,PrazoFinal,SoldadoId")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            ViewData["Status"] = new SelectList(Enum.GetValues<StatusOrdem>(), ordem.Status);
            return View(ordem);
        }

        // GET: Ordens/Edit/5
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem == null)
            {
                return NotFound();
            }
            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            // --- CORRIGIDO: Sintaxe de Enum ---
            ViewData["Status"] = new SelectList(Enum.GetValues<StatusOrdem>(), ordem.Status);
            return View(ordem);
        }

        // POST: Ordens/Edit/5
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,Descricao,Status,DataEmissao,PrazoFinal,SoldadoId")] Ordem ordem)
        {
            if (id != ordem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdemExists(ordem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            ViewData["Status"] = new SelectList(Enum.GetValues<StatusOrdem>(), ordem.Status);
            return View(ordem);
        }

        // GET: Ordens/Delete/5
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordem = await ObterOrdensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // POST: Ordens/Delete/5
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordem = await _context.Ordens.FindAsync(id);
            if (ordem != null)
            {
                _context.Ordens.Remove(ordem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdemExists(int id)
        {
            return _context.Ordens.Any(e => e.Id == id);
        }
    }
}