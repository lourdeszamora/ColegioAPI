using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Infraestructure
{
    public class AlumnoRepository(ColegioContext context)
    {
        private readonly ColegioContext _context=context;
        public async Task<List<Alumno>> GetAll(int page, int pageSize)
        {
            if (page == 1)
            {
                return await _context.Alumnos.Take(pageSize).ToListAsync();
            }
            return await _context.Alumnos.Skip(page -1 * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Alumno?> GetById(string id)
        {
            return await _context.Alumnos.FindAsync(id);
        }

        public async Task<AlumnoGrado?> GetGrado(string id)
        {
            return await _context.AlumnosGrados.FirstOrDefaultAsync(ag => ag.AlumnoId == id);
        }

        public async Task<AlumnoGrado> AddGrado(AlumnoGrado alumnoGrado)
        {
            var alumno = await GetById(alumnoGrado.AlumnoId);
            if (alumno is null)
            {
                throw new Exception("Alumno no encontrado");
            }
            var grado = await _context.Grados.FindAsync(alumnoGrado.GradoId);
            if (grado is null)
            {
                throw new Exception("Grado no encontrado");
            }
            _context.AlumnosGrados.Add(alumnoGrado);
            await _context.SaveChangesAsync();
            return alumnoGrado;
        }

        public async Task<Alumno> Create(Alumno alumno)
        {
            if (await GetById(alumno.Id) is not null)
            {
                throw new Exception("El alumno ya esta inscrito");
            }
            _context.Alumnos.Add(alumno);
            await _context.SaveChangesAsync();
            return alumno;
        }

        public async Task<Alumno> Update(Alumno alumno)
        {
            var a = await GetById(alumno.Id);
            if (a is null)
            {
                throw new Exception("Alumno no encontrado");
            }
            _context.Entry(a).State = EntityState.Detached;
            _context.Alumnos.Update(alumno);
            await context.SaveChangesAsync();
            return alumno;
        }

        public async Task Delete(string id)
        {
            var alumno = await GetById(id);
            if (alumno is null)
            {
                throw new Exception("Alumno no encontrado");
            }
            context.Alumnos.Remove(alumno);
            await context.SaveChangesAsync();
        }

        public async Task<int> Count()
        {
            return await _context.Alumnos.CountAsync();
        }
    }
}
