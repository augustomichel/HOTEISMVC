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
    public class HotelsController : Controller
    {
        private readonly Contexto _context;

        public HotelsController(Contexto context)
        {
            _context = context;
        }

        
        /// <summary>
        /// Direciona para Hoteis e carrega lista
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Hotel.ToListAsync());
        }

        
        /// <summary>
        /// Abre tela de detalhes do hotel
        /// </summary>
        /// <param name="id">Id do Hotel</param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //Buscar Lista de Hoteis
            var hotel = await _context.Hotel
                .FirstOrDefaultAsync(m => m.Id == id);

            //Popular Lista de Quartos
            hotel.Quartos = GetQuartos(id);

            //Popular Lista de Fotos
            hotel.Fotos = GetFotos(id);

            if (hotel == null)
            {
                return NotFound();
            }

            //Popula Model para exibir cabeçalhos na View
            var hotelDetail = new HotelDetail();
            hotelDetail.Hotel = hotel;

            return View(hotelDetail);
        }

        
        /// <summary>
        /// Abre tela de cadastro de hotel
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        
        /// <summary>
        /// Realiza cadastro do hotel
        /// </summary>
        /// <param name="hotel">Model contendo bind do form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CNPJ,Endereco,Descricao")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hotel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        
        /// <summary>
        /// Abre tela de Editar hotel
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel.FindAsync(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

       
        /// <summary>
        /// Executa update do hotel
        /// </summary>
        /// <param name="id">Id do hotel</param>
        /// <param name="hotel">Model do bind do form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CNPJ,Endereco,Descricao")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hotel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            return View(hotel);
        }

        
        /// <summary>
        /// Abre tela de deletar hotel
        /// </summary>
        /// <param name="id">Id Hotel</param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hotel = await _context.Hotel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hotel == null)
            {
                return NotFound();
            }

            return View(hotel);
        }

        
        /// <summary>
        /// Realiza Delete do hotel
        /// </summary>
        /// <param name="id">Id Hotel</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = await _context.Hotel.FindAsync(id);
            _context.Hotel.Remove(hotel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return _context.Hotel.Any(e => e.Id == id);
        }


        /// <summary>
        /// Consulta Quartos do Hotel
        /// </summary>
        /// <param name="id">Id do Hotel</param>
        /// <returns>Lista de Quartos</returns>
        private  List<Quarto> GetQuartos(int? id)
        {
            var contexto = _context.Quarto.Where(q => q.Id_Hotel == id);
            return contexto.ToList();
        }


        /// <summary>
        /// Retorna Lista de Fotos do Hotel
        /// </summary>
        /// <param name="id">Id do Hotel</param>
        /// <returns>Lista de Fotos</returns>
        private List<Fotos> GetFotos(int? id)
        {
            var contexto = _context.Fotos.Where(q => q.Id_Hotel == id);
            return contexto.ToList();
        }
    }
}
