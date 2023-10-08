using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FakeCfdiGenerator.Backend.DataAccess;

namespace FakeCfdiGenerator.Backend.Catalogs.Contribuyente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContribuyenteController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public ContribuyenteController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: api/Contribuyente
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataAccess.Contribuyente>>> GetContribuyentes()
        {
            if (_context.Contribuyentes == null)
            {
                return NotFound();
            }

            return await _context.Contribuyentes.ToListAsync();
        }

        // GET: api/Contribuyente/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DataAccess.Contribuyente>> GetContribuyente(int id)
        {
            if (_context.Contribuyentes == null)
            {
                return NotFound();
            }

            var contribuyente = await _context.Contribuyentes.FindAsync(id);

            if (contribuyente == null)
            {
                return NotFound();
            }

            return contribuyente;
        }

        // PUT: api/Contribuyente/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContribuyente(int id, DataAccess.Contribuyente contribuyente)
        {
            if (id != contribuyente.Id)
            {
                return BadRequest();
            }

            _context.Entry(contribuyente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContribuyenteExists(id))
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

        // POST: api/Contribuyente
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<DataAccess.Contribuyente>> PostContribuyente(
            DataAccess.Contribuyente contribuyente)
        {
            if (_context.Contribuyentes == null)
            {
                return Problem("Entity set 'ApplicationContext.Contribuyentes'  is null.");
            }

            _context.Contribuyentes.Add(contribuyente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContribuyente", new { id = contribuyente.Id }, contribuyente);
        }

        // DELETE: api/Contribuyente/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContribuyente(int id)
        {
            if (_context.Contribuyentes == null)
            {
                return NotFound();
            }

            var contribuyente = await _context.Contribuyentes.FindAsync(id);
            if (contribuyente == null)
            {
                return NotFound();
            }

            _context.Contribuyentes.Remove(contribuyente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContribuyenteExists(int id)
        {
            return (_context.Contribuyentes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}