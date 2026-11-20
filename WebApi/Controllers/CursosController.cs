using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Dtos;
using WebApi.Models;

[Route("api/[controller]")]
[ApiController]
public class CursosController : ControllerBase
{
    private readonly WebApiContext _context;
    private readonly IMapper _mapper;
    public CursosController(WebApiContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    // GET: api/Curso
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CursoDto>>> GetCurso()
    {
        var m = await _context.Curso.ToListAsync();
        var dtos = _mapper.Map<List<Curso>, List<CursoDto>>(m);
        return dtos;
    }

    // GET: api/Curso/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CursoDto>> GetCurso(int id)
    {
        var curso = await _context.Curso.FindAsync(id);

        if (curso == null)
        {
            return NotFound();
        }
        var dto = _mapper.Map<CursoDto>(curso);
        return dto;
    }

    // PUT: api/Curso/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCurso(int? id, CursoDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }
        var db = await _context.Curso.FindAsync(id);
        if (db == null)
        {
            return NotFound();
        }
        _mapper.Map<CursoDto, Curso>(dto, db);
        _context.Entry(db).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CursoExists(id))
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

    // POST: api/Curso
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Curso>> PostCurso(CursoDto dto)
    {
        var db = _mapper.Map<Curso>(dto);
        _context.Curso.Add(db);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetCurso", new { id = dto.Id }, dto);
    }

    // DELETE: api/Curso/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCurso(int? id)
    {
        var curso = await _context.Curso.FindAsync(id);
        if (curso == null)
        {
            return NotFound();
        }

        _context.Curso.Remove(curso);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool CursoExists(int? id)
    {
        return _context.Curso.Any(e => e.Id == id);
    }
}
