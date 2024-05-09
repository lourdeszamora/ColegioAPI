
using ColegioAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ColegioAPI.Infraestructure
{
    public class ColegioContext: DbContext
    {
        public ColegioContext(DbContextOptions<ColegioContext> options) : base(options)
        {
        }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Grado> Grados { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<AlumnoGrado> AlumnosGrados { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AlumnoGrado>()
                .HasKey(ag => new { ag.Id});

            modelBuilder.Entity<AlumnoGrado>()
                .HasOne(ag => ag.Alumno)
                .WithMany(a => a.AlumnosGrados)
                .HasForeignKey(ag => ag.AlumnoId);

            modelBuilder.Entity<AlumnoGrado>()
                .HasOne(ag => ag.Grado)
                .WithMany(g => g.AlumnosGrados)
                .HasForeignKey(ag => ag.GradoId);
        }
    }
}
