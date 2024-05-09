using System.ComponentModel.DataAnnotations;

namespace ColegioAPI.Models
{
    public class Alumno
    {
        [StringLength(13, ErrorMessage = "La identidad debe contener 13 digitos")]
        [Key]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public List<AlumnoGrado> AlumnosGrados { get; set; }
    }
}
