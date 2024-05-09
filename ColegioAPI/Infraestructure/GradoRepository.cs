using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Infraestructure
{
    public class GradoRepository
    {
        private readonly ColegioContext _context;

        public GradoRepository(ColegioContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Grado>> GetAll()
        {
            return await _context.Grados.ToListAsync();
        }

        public async Task<Grado?> GetById(int id)
        {
            return await _context.Grados.FindAsync(id);
        }

        public async Task<Grado> Create(Grado grado)
        {
            
            var profesor = await _context.Profesores.FindAsync(grado.ProfesorId);
            if (profesor is null)
            {
                throw new Exception("Profesor no encontrado");
            }
            _context.Grados.Add(grado);
            await _context.SaveChangesAsync();
            return grado;
        }

        public async Task Update(Grado grado)
        {
            var g = await GetById(grado.Id);
            if (g is null)
            {
                throw new Exception("Grado no encontrado");
            }
            _context.Grados.Update(grado);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var gradoToDelete = await _context.Grados.FindAsync(id);
            if (gradoToDelete is null)
            {
                throw new Exception("Grado no encontrado");
            }
            _context.Grados.Remove(gradoToDelete);
            await _context.SaveChangesAsync();
        }
    }
}
