using BackEndProyectoSano.DBaseContext;
using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RutinaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RutinaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rutina>>> GetRutinas()
        {
            return await _context.Rutinas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rutina>> GetRutina(int id)
        {
            var rutina = await _context.Rutinas.FindAsync(id);

            if (rutina == null)
            {
                return NotFound();
            }

            return rutina;
        }

        [HttpGet("userId/{id}")]
        public async Task<ActionResult<List<Rutina>>> GetRutinasByUserId(int id)
        {
            var rutinas = await _context.Rutinas
                                               .Where(a => a.UserId == id)
                                               .ToListAsync();

            if (rutinas == null || !rutinas.Any())
            {
                return NotFound();
            }

            return rutinas;
        }

        [HttpPost]
        public async Task<ActionResult<Rutina>> PostRutina(Rutina rutina)
        {
            _context.Rutinas.Add(rutina);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRutina", new { id = rutina.Id }, rutina);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRutina(int id, Rutina rutina)
        {
            if (id != rutina.Id)
            {
                return BadRequest();
            }

            _context.Entry(rutina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RutinaExists(id))
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
        public async Task<IActionResult> DeleteRutina(int id)
        {
            var rutina = await _context.Rutinas.FindAsync(id);
            if (rutina == null)
            {
                return NotFound();
            }

            _context.Rutinas.Remove(rutina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RutinaExists(int id)
        {
            return _context.Rutinas.Any(e => e.Id == id);
        }
    }
}
