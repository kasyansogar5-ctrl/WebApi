using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class NivelesController : ControllerBase
{
    private readonly WebApiContext _context;
    private readonly IMapper _mapper;
    public NivelesController(WebApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Niveles
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Nivel>>> GetNiveles()
    {
        return await _context.Nivel.ToListAsync();
    }

    // GET: api/Niveles/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Nivel>> GetNiveles(int id)
    {
        var niveles = await _context.Nivel.FindAsync(id);

        if (niveles == null)
        {
            return NotFound();
        }

        return niveles;
    }

    // PUT: api/Niveles/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNiveles(int? id, Nivel niveles)
    {
        if (id != niveles.Id)
        {
            return BadRequest();
        }

        _context.Entry(niveles).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!NivelesExists(id))
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

    // POST: api/Niveles
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Nivel>> PostNiveles(Nivel niveles)
    {
        _context.Nivel.Add(niveles);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetNiveles", new { id = niveles.Id }, niveles);
    }

    // DELETE: api/Niveles/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNiveles(int? id)
    {
        var niveles = await _context.Nivel.FindAsync(id);
        if (niveles == null)
        {
            return NotFound();
        }

        _context.Nivel.Remove(niveles);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    //GET: api/Nivel/5/Cursos
    [HttpGet("{id}/Cursos")]
    public async Task<ActionResult<List<CursoDto>>> GetCursosByNivel(int id)
    {
        var nivel = await _context.Nivel.FirstAsync();
        if (nivel == null)
        {
            return NotFound();
        }
        var cursos = _context.Curso.Where(c => c.NivelId == id).ToList();
        var cursosDto = _mapper.Map<List<CursoDto>>(cursos);
        return cursosDto;
    }

    private bool NivelesExists(int? id)
    {
        return _context.Nivel.Any(e => e.Id == id);
    }
}
