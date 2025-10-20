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
    public class SoldadosController : Controller
    {
        private readonly ContextoWarhammer _context;

        public SoldadosController(ContextoWarhammer context)
        {
            _context = context;
        }

     
        private IQueryable<Soldado> ObterSoldadosComRelacoes()
        {
            return _context.Soldados
                .Include(s => s.ItensDeBatalha)
                .Include(s => s.Missoes)
                .Include(s => s.Treinamentos)
                .Include(s => s.OrdensRecebidas);
        }

     
        // GET: Soldados/Index
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soldados.ToListAsync());
        }

        // GET: Soldados/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await ObterSoldadosComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (soldado == null)
            {
                return NotFound();
            }

            return View(soldado);
        }

        // GET: Soldados/Create

        public IActionResult Create()
        {
            ViewData["Capitulo"] = new SelectList(Enum.GetValues(typeof(Capitulo)));
            ViewData["Patente"] = new SelectList(Enum.GetValues(typeof(Patente)));
            return View();
        }

        // POST: Soldados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Capitulo,Patente,Altura,Peso")] Soldado soldado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["Capitulo"] = new SelectList(Enum.GetValues(typeof(Capitulo)), soldado.Capitulo);
            ViewData["Patente"] = new SelectList(Enum.GetValues(typeof(Patente)), soldado.Patente);
            return View(soldado);
        }

        // GET: Soldados/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await _context.Soldados.FindAsync(id);

            if (soldado == null)
            {
                ViewData["BodyClass"] = "theme-ultramarine";
                return NotFound();
            }

            // CRUCIAL: Passa o valor atual (soldado.Capitulo/Patente) para que a opção seja pré-selecionada no formulário.
            ViewData["Capitulo"] = new SelectList(Enum.GetValues(typeof(Capitulo)), soldado.Capitulo);
            ViewData["Patente"] = new SelectList(Enum.GetValues(typeof(Patente)), soldado.Patente);

            return View(soldado);
        }

        // POST: Soldados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Capitulo,Patente,Altura,Peso")] Soldado soldado)
        {
            if (id != soldado.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(soldado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SoldadoExists(soldado.Id))
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
            ViewData["Capitulo"] = new SelectList(Enum.GetValues(typeof(Capitulo)), soldado.Capitulo);
            ViewData["Patente"] = new SelectList(Enum.GetValues(typeof(Patente)), soldado.Patente);

            return View(soldado);
        }

        // GET: Soldados/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await ObterSoldadosComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (soldado == null)
            {
                return NotFound();
            }

            return View(soldado);
        }

        // POST: Soldados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var soldado = await _context.Soldados.FindAsync(id);

            if (soldado != null)
            {
                _context.Soldados.Remove(soldado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SoldadoExists(int id)
        {
            return _context.Soldados.Any(e => e.Id == id);
        }
    }
}