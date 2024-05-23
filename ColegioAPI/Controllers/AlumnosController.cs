using ColegioAPI.DTO;
using ColegioAPI.Infraestructure;
using ColegioAPI.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ColegioAPI.Controllers
{
    [Route("api/alumnos")]
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
            var dtos = ans.Select(a => new AlumnoDTO
            {
                Id = a.Id,
                Nombre = a.Nombre,
                Apellidos = a.Apellidos,
                Genero = a.Genero,
                FechaNacimiento = a.FechaNacimiento
            }).ToList();
            var count = await _repository.Count();
            var paginable = new Paginable<AlumnoDTO>
            {
                Page = page,
                PageSize = pageSize,
                Data = dtos.ToList(),
                Total = count
            };
            return  Ok(paginable);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>  Get([FromRoute]string id)
        {
            var ans = await _repository.GetById(id);
            var dto = new AlumnoDTO
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero,
                FechaNacimiento = ans.FechaNacimiento
            };
            if (ans is null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        [HttpGet("{id}/grado")]
        public async Task<IActionResult> GetGrados([FromRoute] string id)
        {
            var al = await _repository.GetById(id);
            if (al is null)
            {
                return NotFound();
            }

            var ans = (await _repository.GetGrado(id));
            if (ans is null)
            {
                return NotFound("El alumno no esta inscrito en un grado");
            }
            var alumnoGrado =  new AlumnoGradoDTO
            {
                Grado = new GradoDTO
                {
                    Id = ans.Grado.Id,
                    Nombre = ans.Grado.Nombre,
                    ProfesorId = ans.Grado.ProfesorId
                },
                Seccion = ans.Seccion
            };
            return Ok(alumnoGrado);
        }

        [HttpPost("{id}/grado")]
        public async Task<IActionResult> PostGrado([FromRoute] string id, [FromBody] AlumnoGradoDTO alumnoGrado)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _repository.GetById(id) is null)
            {
                return NotFound("No se ha encontrado el alumno");
            }

            if (await _repository.GetGrado(id) is not null)
            {
                return BadRequest("El alumno ya esta inscrito en un grado");
            }

            var ans = await _repository.AddGrado(new AlumnoGrado
            {
                AlumnoId = id,
                GradoId = alumnoGrado.GradoId,
                Seccion = alumnoGrado.Seccion
            });
            var dto = new AlumnoGradoDTO
            {
                Grado = new GradoDTO
                {
                    Id = ans.Grado.Id,
                    Nombre = ans.Grado.Nombre,
                    ProfesorId = ans.Grado.ProfesorId
                },
                Seccion = ans.Seccion
            };
            return CreatedAtAction("PostGrado", dto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AlumnoDTO alumno)
        {
            var alum = await _repository.GetById(alumno.Id);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (alum is not null)
            {
                return BadRequest("El alumno ya esta inscrito");
            }
            var ans = await _repository.Create(new Alumno
            {
                Id = alumno.Id,
                Nombre = alumno.Nombre,
                Apellidos = alumno.Apellidos,
                Genero = alumno.Genero,
                FechaNacimiento = alumno.FechaNacimiento
            });
            var dto = new AlumnoDTO
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero,
                FechaNacimiento = ans.FechaNacimiento
            };
            return CreatedAtAction("Post", dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] AlumnoDTO alumno)
        {
            if (id != alumno.Id)
            {
                return BadRequest("No se puede actualizar el ID de registro");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
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
            var dto = new AlumnoDTO
            {
                Id = ans.Id,
                Nombre = ans.Nombre,
                Apellidos = ans.Apellidos,
                Genero = ans.Genero,
                FechaNacimiento = ans.FechaNacimiento
            };
            return Ok(dto);
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
