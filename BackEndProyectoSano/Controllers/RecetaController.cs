using BackEndProyectoSano.DBaseContext;
using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecetaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RecetaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            return await _context.Recetas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        [HttpGet("Categoria/{categoria}")]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetasByCategoria(string categoria)
        {
            var recetas = await _context.Recetas
                .Where(r => r.Categoria == categoria)
                .ToListAsync();

            if (recetas == null || !recetas.Any())
            {
                return NotFound(new { Message = $"No se encontraron recetas en la categoría '{categoria}'." });
            }

            return recetas;
        }

        [HttpPost]
        public async Task<ActionResult<Receta>> CreateReceta(Receta receta)
        {
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetReceta), new { id = receta.Id }, receta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReceta(int id, Receta receta)
        {
            if (id != receta.Id)
            {
                return BadRequest();
            }

            _context.Entry(receta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecetaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
            {
                return NotFound();
            }

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecetaExists(int id)
        {
            return _context.Recetas.Any(e => e.Id == id);
        }
    }
}
