using System.ComponentModel.DataAnnotations.Schema;

namespace ColegioAPI.Models
{
    public class Grado
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ProfesorId { get; set; }

        public Profesor Profesor { get; set; }
        public List<AlumnoGrado> AlumnosGrados { get; set; }
    }
}
