using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zircon_Hobbies.Data;
using Zircon_Hobbies.Models;

namespace Zircon_Hobbies.Controllers
{

	[Route("api/Gunpla")]
	[ApiController]
	public class GunplaApiController : ControllerBase
	{

		private readonly Zircon_HobbiesContext _context;

		public GunplaApiController(Zircon_HobbiesContext context)
		{
			_context = context;
		}

		[HttpGet]
		public async Task<ActionResult<IEnumerable<Gunpla>>> GetGunplas()
		{
			var gunplas = await _context.Gunpla.Include(g => g.ProductionCompany).ToListAsync();

			var result = gunplas.Select(g => new GunplaDTO
			{

                Id = g.Id,
                Name = g.Name,
                Type = g.Type,
                Scale = g.Scale,
                Price = g.Price,
                ProductionCompanyName = g.ProductionCompany?.Name

            });
			return Ok(result);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<Gunpla>> GetGunpla(int id)
		{

			var gunpla = await _context.Gunpla.Include(g => g.ProductionCompany)
				.FirstOrDefaultAsync(g => g.Id == id);

			if (gunpla == null)
				return NotFound();

			return gunpla;
		}

        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<Gunpla>>> GetByName(string name)
        {
            var result = await _context.Gunpla
                .Include(g => g.ProductionCompany)
                .Where(g => g.Name.Contains(name))
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("ByType/{type}")]
        public async Task<ActionResult<IEnumerable<Gunpla>>> GetByType(string type)
        {

            // Tried using typemap to catch people using the full name of the grade
            // "Real Grade" instead of just "RG" but couldn't get it to work

            //var typemap = new Dictionary<string, string>()
            //{
            //    {"SD","Super Deformed" },
            //    {"HG","High Grade"},
            //    {"HGUC","High Grade Universal Century"},
            //    {"RG","Real Grade"},
            //    {"MG","Master Grade"},
            //    {"PG","Perfect Grade"},

            //};

            //if (!typemap.TryGetValue(type, out var fulltype))
            //{
            //    return BadRequest($"Unknown Type: {type}.");
            //}

            var result = await _context.Gunpla
                .Include(g => g.ProductionCompany)
                .Where(g => g.Type.Equals(type))
                .ToListAsync();

            return Ok(result);
        }

        [HttpGet("ByScale")]
        public async Task<ActionResult<IEnumerable<Gunpla>>> GetByScale([FromQuery] string scale)
        {
            var result = await _context.Gunpla
                .Include(g => g.ProductionCompany)
                .Where(g => g.Scale.Equals(scale))
                .ToListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<Gunpla>> PostGunpla(GunplaPostDto dto)
        {
            var gunpla = new Gunpla
            {
                Name = dto.Name,
                Type = dto.Type,
                Scale = dto.Scale,
                Price = dto.Price,
                ProductionCompanyId = dto.ProductionCompanyId
            };

            _context.Gunpla.Add(gunpla);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetGunpla), new { id = gunpla.Id }, gunpla);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGunpla(int id, GunplaPutDto dto)
        {
            if (id != dto.Id)
                return BadRequest();

            var existingGunpla = await _context.Gunpla.FindAsync(id);
            if (existingGunpla == null)
                return NotFound();

            // Update properties
            existingGunpla.Name = dto.Name;
            existingGunpla.Type = dto.Type;
            existingGunpla.Scale = dto.Scale;
            existingGunpla.Price = dto.Price;
            existingGunpla.ProductionCompanyId = dto.ProductionCompanyId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Gunpla.Any(g => g.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGunpla(int id)
		{
			var gunpla = await _context.Gunpla.FindAsync(id);
			if (gunpla == null)
				return NotFound();

			_context.Gunpla.Remove(gunpla);
			await _context.SaveChangesAsync();

			return NoContent();
		}

	}
}
