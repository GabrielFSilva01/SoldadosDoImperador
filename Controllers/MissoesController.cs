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
    public class MissoesController : Controller
    {
        private readonly ContextoWarhammer _context;

        public MissoesController(ContextoWarhammer context)
        {
            _context = context;
        }

        private IQueryable<Missao> ObterMissoesComRelacoes()
        {
            // Retorna a consulta base com a inclusão do Soldado.
            return _context.Missoes.Include(m => m.Soldado);
        }

       
        // GET: Missoes
        
        public async Task<IActionResult> Index()
        {
            return View(await ObterMissoesComRelacoes().ToListAsync());
        }

        // GET: Missoes/Details/5
      
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missao = await ObterMissoesComRelacoes() 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (missao == null)
            {
                return NotFound();
            }

            return View(missao);
        }

        // GET: Missoes/Create
        public IActionResult Create()
        {
         
            ViewData["SoldadoId"] = new SelectList(_context.Soldados, "Id", "Nome");
            return View();
        }

        // POST: Missoes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Objetivo,Status,DataInicio,Localizacao,DataFim,SoldadoId")] Missao missao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(missao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", missao.SoldadoId);
            return View(missao);
        }

        // GET: Missoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Usamos FindAsync pois não precisamos da relação aqui
            var missao = await _context.Missoes.FindAsync(id);
            if (missao == null)
            {
                return NotFound();
            }
            
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", missao.SoldadoId);
            return View(missao);
        }

        // POST: Missoes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Objetivo,Status,DataInicio,Localizacao,DataFim,SoldadoId")] Missao missao)
        {
            if (id != missao.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Lógica de atualização e concorrência
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
                return RedirectToAction(nameof(Index));
            }
            ViewData["Soldado escalado para a missão"] = new SelectList(_context.Soldados, "Id", "Nome", missao.SoldadoId);
            return View(missao);
        }

        // GET: Missoes/Delete/5
       
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var missao = await ObterMissoesComRelacoes() 
                .FirstOrDefaultAsync(m => m.Id == id);

            if (missao == null)
            {
                return NotFound();
            }

            return View(missao);
        }

        // POST: Missoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var missao = await _context.Missoes.FindAsync(id);
            if (missao != null)
            {
                _context.Missoes.Remove(missao);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissaoExists(int id)
        {
            return _context.Missoes.Any(e => e.Id == id);
        }
    }
}