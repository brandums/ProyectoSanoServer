using BackEndProyectoSano.DBaseContext;
using BackEndProyectoSano.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BackEndProyectoSano.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlimentacionController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AlimentacionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alimentacion>>> GetAlimentaciones()
        {
            return await _context.Alimentaciones.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Alimentacion>> GetAlimentacion(int id)
        {
            var alimentacion = await _context.Alimentaciones.FindAsync(id);

            if (alimentacion == null)
            {
                return NotFound();
            }

            return alimentacion;
        }

        [HttpGet("userId/{id}")]
        public async Task<ActionResult<List<Alimentacion>>> GetAlimentacionByUserId(int id)
        {
            var alimentaciones = await _context.Alimentaciones
                                               .Where(a => a.UserId == id)
                                               .ToListAsync();

            if (alimentaciones == null || !alimentaciones.Any())
            {
                return NotFound();
            }

            return alimentaciones;
        }

        [HttpPost]
        public async Task<ActionResult<Alimentacion>> CreateAlimentacion(Alimentacion alimentacion)
        {
            _context.Alimentaciones.Add(alimentacion);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAlimentacion), new { id = alimentacion.Id }, alimentacion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAlimentacion(int id, Alimentacion alimentacion)
        {
            if (id != alimentacion.Id)
            {
                return BadRequest();
            }

            _context.Entry(alimentacion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentacionExists(id))
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
        public async Task<IActionResult> DeleteAlimentacion(int id)
        {
            var alimentacion = await _context.Alimentaciones.FindAsync(id);
            if (alimentacion == null)
            {
                return NotFound();
            }

            _context.Alimentaciones.Remove(alimentacion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlimentacionExists(int id)
        {
            return _context.Alimentaciones.Any(e => e.Id == id);
        }
    }
}