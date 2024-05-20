using System.Web.Http.Cors;
using ColegioAPI.DTO;
using ColegioAPI.Infraestructure;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/profesores")]
    [ApiController]
    public class ProfesoresController : ControllerBase
    {
        private readonly ProfesorRepository _repository;

        public ProfesoresController(ProfesorRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int pageSize = 20)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            var ans = await _repository.GetAll(page, pageSize);
            var dtos = ans.Select(profesor => new ProfesorDTO()
            {
                Id = profesor.Id,
                Nombre = profesor.Nombre,
                Apellidos = profesor.Apellidos,
                Genero = profesor.Genero
            }).ToList();
            return Ok(dtos);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
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
            var dto = new ProfesorDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero
            };
            return CreatedAtAction("Post",ans);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] ProfesorDTO profesor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var prof = await _repository.GetById(id);
            if (prof is null)
            {
                return NotFound("No se encontro el profesor");
            }
            var ans = await _repository.Update( new Profesor()
            {
                Id = id,
                Nombre = profesor.Nombre,
                Apellidos = profesor.Apellidos,
                Genero = profesor.Genero
            });
            
            var dto = new ProfesorDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero
            };
            return Ok(dto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            var ans = await _repository.GetById(id);
            if (ans is null)
            {
                return NotFound();
            }
            await _repository.Delete(id);
            var dto = new ProfesorDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero
            };
            return Ok(dto);
        }   
    }
}
