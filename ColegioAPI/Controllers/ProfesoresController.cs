using ColegioAPI.DTO;
using ColegioAPI.Infraestructure;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly ProfesorRepository _repository;

        public ProfesoresController(ProfesorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            var ans = await _repository.GetAll(page, pageSize);
            return Ok(ans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        {
            var ans = await _repository.GetById(id);
            if (ans is null)
            {
                return NotFound();
            }

            var dto = new ProfesorDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProfesorDTO profesor)
        {
            var prof = await _repository.GetById(profesor.Id);
            if (prof is not null)
            {
                return Conflict("Hay otro profesor registrado con esa identidad");
            }
            var ans = await _repository.Create(new Profesor()
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellidos = profesor.Apellidos,
                Genero = profesor.Genero
            });
            return CreatedAtAction("Post",ans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] ProfesorDTO profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var ans = await _repository.Update( new Profesor()
            {
                Id = id,
                Nombre = profesor.Nombre,
                Apellidos = profesor.Apellidos,
                Genero = profesor.Genero
            });
            if (ans is null)
            {
                return NotFound();
            }
            return Ok(ans);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var ans = _repository.GetById(id);
            if (ans is null)
            {
                return NotFound();
            }
            await _repository.Delete(id);
            return Ok(ans);
        }   
    }
}
