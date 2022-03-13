#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOTEISMVC.Models;

namespace HOTEISMVC.Controllers
{
    public class QuartosController : Controller
    {
        private readonly Contexto _context;

        public QuartosController(Contexto context)
        {
            _context = context;
        }

        
        /// <summary>
        /// Abre tela contendo lista de quartos
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Quarto.Include(q => q.Hotel);
            return View(await contexto.ToListAsync());
        }

        /// <summary>
        /// Abre tela de detalhes do quarto
        /// </summary>
        /// <param name="id">Id Quarto</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto
                .Include(q => q.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }


        
        /// <summary>
        /// Abre tela de cadastro de Quarto
        /// </summary>
        /// <param name="id">Id Hotel</param>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            //verifica se existe Id do hotel
            if (id == null)
            {
                //caso não exista a tela abre com o combo para selecionar o hotel
                ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome");
                ViewBag.habilita_combo = true;
            }
            else
            {
                //realiza a seleção automatica do hotel no qual o quarto pertence
                ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", id);
                ViewBag.habilita_combo  = false;
            }
            return View();
        }

        
        /// <summary>
        /// Realiza o insert do Quarto
        /// </summary>
        /// <param name="quarto">Model contendo o bind do form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Hotel,Nome,NumOcupantes,NumAdultos,NumCriancas,Preco")] Quarto quarto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(quarto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", quarto.Id_Hotel );
            return View(quarto);
        }

        
        /// <summary>
        /// Abre tela de editar o quarto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto.FindAsync(id);
            if (quarto == null)
            {
                return NotFound();
            }
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", quarto.Id_Hotel);
            return View(quarto);
        }

     
        /// <summary>
        /// Realiza o Udate do Quarto
        /// </summary>
        /// <param name="id">Id do Quarto</param>
        /// <param name="quarto">Model contendo o bind do form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_Hotel,Nome,NumOcupantes,NumAdultos,NumCriancas,Preco")] Quarto quarto)
        {
            if (id != quarto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quarto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuartoExists(quarto.Id))
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
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", quarto.Id_Hotel);
            return View(quarto);
        }

        
        /// <summary>
        /// Abre tela de deletar quarto
        /// </summary>
        /// <param name="id">Id do Quarto</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quarto = await _context.Quarto
                .Include(q => q.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (quarto == null)
            {
                return NotFound();
            }

            return View(quarto);
        }

        
        /// <summary>
        /// Realiza delete do quarto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quarto = await _context.Quarto.FindAsync(id);
            _context.Quarto.Remove(quarto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool QuartoExists(int id)
        {
            return _context.Quarto.Any(e => e.Id == id);
        }
    
    }
}
