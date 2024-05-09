using ColegioAPI.DTO;
using ColegioAPI.Infraestructure;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColegioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlumnosController : ControllerBase
    {
        private readonly AlumnoRepository _repository;

        public AlumnosController(AlumnoRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]int page, [FromQuery] int pageSize)
        {
            if (page < 1 || pageSize < 1)
            {
                return BadRequest();
            }
            var ans  =  await _repository.GetAll(page,pageSize);
            return  Ok(ans);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  Get([FromRoute]string id)
        {
            var ans = await _repository.GetById(id);
            if (ans is null)
            {
                return NotFound();
            }
            return Ok(ans);
        }

        [HttpPost(Name = "Post" )]
        public async Task<IActionResult> Post([FromBody] AlumnoDTO alumno)
        {
            var alum = _repository.GetById(alumno.Id);
            try
            {
                var ans = await _repository.Create(new Alumno
                {
                    Id = alumno.Id,
                    Nombre = alumno.Nombre,
                    Apellidos = alumno.Apellidos,
                    Genero = alumno.Genero,
                    FechaNacimiento = alumno.FechaNacimiento
                });
                return CreatedAtAction("Post", ans);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AlumnoDTO alumno)
        {
            if (id != alumno.Id)
            {
                return BadRequest();
            }

            if (await _repository.GetById(id) is null)

            {
                return NotFound();
            }
            var ans = await _repository.Update(new Alumno
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellidos = alumno.Apellidos,
                Genero = alumno.Genero,
                FechaNacimiento = alumno.FechaNacimiento
            });
            return Ok(ans);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (await _repository.GetById(id) is null)
            {
                return NotFound();
            }
            await _repository.Delete(id);
            return Ok();
        }
    }
}
