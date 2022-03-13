#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HOTEISMVC.Models;

namespace HOTEISMVC.Controllers
{
    public class FotosController : Controller
    {
        private readonly Contexto _context;

        public FotosController(Contexto context)
        {
            _context = context;
        }


        /// <summary>
        /// Redireciona para pagina de detalhes dos hoteis
        /// </summary>
        /// <param name="id">Id do Hotel</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var contexto = _context.Fotos.Include(q => q.Hotel);
            var contexto = _context.Fotos.Where(q => q.Id_Hotel == id);
            return View(await contexto.ToListAsync());
        }

        /// <summary>
        /// Exibe pagina de detalhes da foto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos
                .Include(f => f.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotos == null)
            {
                return NotFound();
            }

            return View();
        }

        
        /// <summary>
        /// Exibe pagina para cadastrar nova foto
        /// </summary>
        /// <param name="id">Id do Hotel</param>
        /// <returns></returns>
        public IActionResult Create(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", id);
            return View();
        }

        
        /// <summary>
        /// Realiza o cadastro de fotos 
        /// </summary>
        /// <param name="fotos">Recebe bind do form</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Id_Hotel,Nome,CNPJ")] Fotos fotos)
        {
            if (ModelState.IsValid)
            {
                //seta id para 0 para receber autoincremento no BD
                fotos.Id = 0;

                //transforma arquivo em array de bytes
                MemoryStream ms = new MemoryStream();
                Request.Form.Files[0].CopyTo(ms);
                fotos.Img = ms.ToArray();

                //executa insert
                _context.Add(fotos);
                await _context.SaveChangesAsync();

                //redireciona para pagina de detalhes do hotel
                return RedirectToRoute(new { controller = "Hotels", action = "Details", id = fotos.Id_Hotel.ToString() } );
            }
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", fotos.Id_Hotel);
            return View(fotos);
        }

        
        /// <summary>
        /// Exibe pagina de edição da foto
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos.FindAsync(id);
            if (fotos == null)
            {
                return NotFound();
            }
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", fotos.Id_Hotel);
            return View(fotos);
        }

        
        /// <summary>
        /// Executa Update
        /// </summary>
        /// <param name="id">Id da foto</param>
        /// <param name="fotos"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Id_Hotel,Nome,CNPJ")] Fotos fotos)
        {
            if (id != fotos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //transforma arquivo em array de bytes
                    MemoryStream ms = new MemoryStream();
                    Request.Form.Files[0].CopyTo(ms);
                    fotos.Img = ms.ToArray();

                    _context.Update(fotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FotosExists(fotos.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToRoute(new { controller = "Hotels", action = "Details", id = fotos.Id_Hotel.ToString() });
            }
            ViewData["Id_Hotel"] = new SelectList(_context.Hotel, "Id", "Nome", fotos.Id_Hotel);
            return View(fotos);
        }

        
        /// <summary>
        /// Abre tela Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fotos = await _context.Fotos
                .Include(f => f.Hotel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fotos == null)
            {
                return NotFound();
            }

            return View(fotos);
        }

        
        /// <summary>
        /// Executa Delete
        /// </summary>
        /// <param name="id">Id foto</param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fotos = await _context.Fotos.FindAsync(id);
            _context.Fotos.Remove(fotos);
            await _context.SaveChangesAsync();
            return RedirectToRoute(new { controller = "Hotels", action = "Details", id = fotos.Id_Hotel.ToString() });
        }

        private bool FotosExists(int id)
        {
            return _context.Fotos.Any(e => e.Id == id);
        }

       
    }
}
