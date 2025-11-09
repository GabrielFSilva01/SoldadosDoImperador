using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using SoldadosDoImperador.Models;

namespace SoldadosDoImperador.Controllers
{
    [Authorize] 
    public class SoldadosController : Controller
    {
        private readonly ContextoWarhammer _context;

        public SoldadosController(ContextoWarhammer context)
        {
            _context = context;
        }

        // GET: Soldados
        public async Task<IActionResult> Index()
        {
            return View(await _context.Soldados.ToListAsync());
        }

        // GET: Soldados/Details/5
       
        [Authorize(Roles = "PRIMARCH")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await _context.Soldados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soldado == null)
            {
                return NotFound();
            }

            return View(soldado);
        }

        // GET: Soldados/Create
        [Authorize(Roles = "PRIMARCH")] 
        public IActionResult Create()
        {
            ViewData["Patente"] = new SelectList(Enum.GetValues<Patente>());
            ViewData["Capitulo"] = new SelectList(Enum.GetValues<Capitulo>());
            return View();
        }

        // POST: Soldados/Create
        [Authorize(Roles = "PRIMARCH")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Capitulo,Patente,Altura,Peso")] Soldado soldado)
        {
            if (ModelState.IsValid)
            {
                _context.Add(soldado);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Patente"] = new SelectList(Enum.GetValues<Patente>(), soldado.Patente);
            ViewData["Capitulo"] = new SelectList(Enum.GetValues<Capitulo>(), soldado.Capitulo);
            return View(soldado);
        }

        // GET: Soldados/Edit/5
        [Authorize(Roles = "PRIMARCH")] 
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await _context.Soldados.FindAsync(id);
            if (soldado == null)
            {
                return NotFound();
            }
            ViewData["Patente"] = new SelectList(Enum.GetValues<Patente>(), soldado.Patente);
            ViewData["Capitulo"] = new SelectList(Enum.GetValues<Capitulo>(), soldado.Capitulo);
            return View(soldado);
        }

        // POST: Soldados/Edit/5
        [Authorize(Roles = "PRIMARCH")] 
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

            ViewData["Patente"] = new SelectList(Enum.GetValues<Patente>(), soldado.Patente);
            ViewData["Capitulo"] = new SelectList(Enum.GetValues<Capitulo>(), soldado.Capitulo);
            return View(soldado);
        }

        // GET: Soldados/Delete/5
        [Authorize(Roles = "PRIMARCH")] 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var soldado = await _context.Soldados
                .FirstOrDefaultAsync(m => m.Id == id);
            if (soldado == null)
            {
                return NotFound();
            }

            return View(soldado);
        }

        // POST: Soldados/Delete/5
        [Authorize(Roles = "PRIMARCH")] 
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