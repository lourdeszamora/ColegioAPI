using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Infraestructure
{
    public class ProfesorRepository
    {
        private readonly ColegioContext _context;

        public ProfesorRepository(ColegioContext context)
        {
            _context = context;
        }

        public async Task<List<Profesor>> GetAll(int page, int pageSize)
        {
            if (page == 1)
            {
                return await _context.Profesores.Take(pageSize).ToListAsync();
            }
            return await _context.Profesores.Skip(page - 1 * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Profesor?> GetById(string id)
        {
            return await _context.Profesores.FindAsync(id);
        }

        public async Task<Profesor> Create(Profesor profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task<Profesor> Update(Profesor profesor)
        {
            var prof = await _context.Profesores.FindAsync(profesor.Id);
            _context.Entry(prof).State = EntityState.Detached;
            if (prof is null)
            {
                throw new Exception("Profesor no encontrado");
            }
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
            return profesor;
        }

        public async Task Delete(string id)
        {
            var profesorToDelete = await _context.Profesores.FindAsync(id);
            _context.Profesores.Remove(profesorToDelete);
            await _context.SaveChangesAsync();
        }   

        public async Task<int> Count()
        {
            return await _context.Profesores.CountAsync();
        }
    }
}
