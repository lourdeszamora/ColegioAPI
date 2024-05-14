using ColegioAPI.DTO;
using ColegioAPI.Infraestructure;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ColegioAPI.Controllers
{
    [Route("api/grados")]
    [ApiController]
    public class GradosController : ControllerBase
    {
        private readonly GradoRepository _repository;

        public GradosController(GradoRepository repository)
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
            var dtos = ans.Select(grado => new GradoDTO()
            {
                Id = grado.Id,
                Nombre = grado.Nombre,
                ProfesorId = grado.ProfesorId
            }).ToList();
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var ans = await _repository.GetById(id);
            if (ans is null)
            {
                return NotFound();
            }
            var dto = new GradoDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                ProfesorId = ans.ProfesorId
            };
            return Ok(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GradoDTO grado)
        {
            var grad = await _repository.GetById(grado.Id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (grad is not null)
            {
                return Conflict("Ya existe un grado registrado con ese identificador");
            }
            var ans = await _repository.Create(new Grado()
            {
                Nombre = grado.Nombre,
                ProfesorId = grado.ProfesorId
            });
            var dto = new GradoDTO()
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                ProfesorId = ans.ProfesorId
            };
            return CreatedAtAction("Post", dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] GradoDTO grado)
        {
            if (id != grado.Id)
            {
                return BadRequest("El identificador del grado no coincide con el identificador de la ruta");
            }
            var grad = await _repository.GetById(id);
            if (grad is null)
            {
                return NotFound("El grado no existe");
            }

            try
            {
                var ans = await _repository.Update(new Grado()
                {
                    Id = grado.Id,
                    Nombre = grado.Nombre,
                    ProfesorId = grado.ProfesorId
                });
                var dto = new GradoDTO()
                {
                    Id = ans.Id,
                    Nombre = ans.Nombre,
                    ProfesorId = ans.ProfesorId
                };
                return Ok(dto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var grad = await _repository.GetById(id);
            if (grad is null)
            {
                return NotFound();
            }
            await _repository.Delete(id);
            return Ok();
        }
    }
}
