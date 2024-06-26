﻿using ColegioAPI.Models;
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

        public async Task<List<Profesor>> GetAll()
        {
            return await _context.Profesores.ToListAsync();
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
            var prof = await GetById(profesor.Id);
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
    }
}
