using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SoldadosDoImperador.Data;
using SoldadosDoImperador.Models;

namespace SoldadosDoImperador.Controllers
{
    
    public class ItemBatalhasController : Controller
    {
        private readonly ContextoWarhammer _context;

        public ItemBatalhasController(ContextoWarhammer context)
        {
            _context = context;
        }

        private IQueryable<ItemBatalha> ObterItensComRelacoes()
        {
            return _context.ItensBatalha.Include(i => i.Soldado);
        }

  
        // GET: ItemBatalhas
        public async Task<IActionResult> Index()
        {
            
            return View(await ObterItensComRelacoes().ToListAsync());
        }

        // GET: ItemBatalhas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

         
            var itemBatalha = await ObterItensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (itemBatalha == null)
            {
                return NotFound();
            }

            return View(itemBatalha);
        }

        // GET: ItemBatalhas/Create
        public IActionResult Create()
        {
            
            ViewData["Soldado Escalado para missão "] = new SelectList(_context.Soldados, "Id", "Nome");
            return View();
        }

        // POST: ItemBatalhas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Tipo,Peso,Especificacao,SoldadoId")] ItemBatalha itemBatalha)
        {
          
            if (ModelState.IsValid)
            {
                _context.Add(itemBatalha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Repopula ViewData em caso de falha
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", itemBatalha.SoldadoId);
            return View(itemBatalha);
        }

        // GET: ItemBatalhas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            
            var itemBatalha = await _context.ItensBatalha.FindAsync(id);
            if (itemBatalha == null)
            {
                return NotFound();
            }

            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", itemBatalha.SoldadoId);
            return View(itemBatalha);
        }

        // POST: ItemBatalhas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Tipo,Peso,Especificacao,SoldadoId")] ItemBatalha itemBatalha)
        {
            if (id != itemBatalha.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemBatalha);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemBatalhaExists(itemBatalha.Id))
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
           
            ViewData["Soldado Escalado para missão"] = new SelectList(_context.Soldados, "Id", "Nome", itemBatalha.SoldadoId);
            return View(itemBatalha);
        }

        // GET: ItemBatalhas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemBatalha = await ObterItensComRelacoes()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (itemBatalha == null)
            {
                return NotFound();
            }

            return View(itemBatalha);
        }

        // POST: ItemBatalhas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var itemBatalha = await _context.ItensBatalha.FindAsync(id);
            if (itemBatalha != null)
            {
                _context.ItensBatalha.Remove(itemBatalha);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemBatalhaExists(int id)
        {
            return _context.ItensBatalha.Any(e => e.Id == id);
        }
    }
}