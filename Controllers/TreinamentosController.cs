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

        
        private IQueryable<Treinamento> ObterTreinamentosComRelacoes()
        {
          
            return _context.Treinamentos.Include(t => t.Soldado);
        }

        // GET: Treinamentos
        public async Task<IActionResult> Index()
        {
         
            return View(await ObterTreinamentosComRelacoes().ToListAsync());
        }

        // GET: Treinamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var treinamento = await ObterTreinamentosComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (treinamento == null)
            {
                return NotFound();
            }

            return View(treinamento);
        }


        // GET: Treinamentos/Create
        public IActionResult Create()
        {
            // CORRIGIDO: Usa a chave SoldadoId (convenção) e exibe o Nome do Soldado
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome");
            return View();
        }

        // POST: Treinamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Area,DuracaoHoras,DataRealizacao,SoldadoId")] Treinamento treinamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treinamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopula ViewData na falha
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", treinamento.SoldadoId);
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
         
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", treinamento.SoldadoId);
            return View(treinamento);
        }

        // POST: Treinamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Area,DuracaoHoras,DataRealizacao,SoldadoId")] Treinamento treinamento)
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
                return RedirectToAction(nameof(Index));
            }
            // Repopula ViewData na falha
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", treinamento.SoldadoId);
            return View(treinamento);
        }

        // GET: Treinamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

           
            var treinamento = await ObterTreinamentosComRelacoes()
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
                _context.Treinamentos.Remove(treinamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreinamentoExists(int id)
        {
            return _context.Treinamentos.Any(e => e.Id == id);
        }
    }
}