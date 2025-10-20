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
    public class OrdensController : Controller
    {
        private readonly ContextoWarhammer _context;

        public OrdensController(ContextoWarhammer context)
        {
            _context = context;
        }
        private IQueryable<Ordem> ObterOrdensComRelacoes()
        {
    
            return _context.Ordens.Include(o => o.Soldado);
        }


        // GET: Ordens
        public async Task<IActionResult> Index()
        {
            // USA O MÉTODO REUSÁVEL
            return View(await ObterOrdensComRelacoes().ToListAsync());
        }

        // GET: Ordens/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // USA O MÉTODO REUSÁVEL
            var ordem = await ObterOrdensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // GET: Ordens/Create
        public IActionResult Create()
        {
        
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome");
            return View();
        }

        // POST: Ordens/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,Descricao,Status,DataEmissao,PrazoFinal,SoldadoId")] Ordem ordem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Repopula ViewData na falha
            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            return View(ordem);
        }

        // GET: Ordens/Edit/5
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
            // CORRIGIDO: Repopula ViewData
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            return View(ordem);
        }

        // POST: Ordens/Edit/5
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
            
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", ordem.SoldadoId);
            return View(ordem);
        }

        // GET: Ordens/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // USA O MÉTODO REUSÁVEL
            var ordem = await ObterOrdensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ordem == null)
            {
                return NotFound();
            }

            return View(ordem);
        }

        // POST: Ordens/Delete/5
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