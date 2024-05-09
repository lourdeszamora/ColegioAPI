using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Infraestructure
{
    public class AlumnoRepository(ColegioContext context)
    {
        private readonly ColegioContext _context=context;
        public async Task<List<Alumno>> GetAll()
        {
            return await _context.Alumnos.ToListAsync();
        }

        public async Task<Alumno?> GetById(string id)
        {
            return await _context.Alumnos.FindAsync(id);
        }

        public async Task<Alumno> Create(Alumno alumno)
        {
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
    }
}
