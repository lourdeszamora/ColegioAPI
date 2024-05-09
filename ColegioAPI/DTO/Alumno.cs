using System.ComponentModel.DataAnnotations;
using ColegioAPI.Models;

namespace ColegioAPI.DTO
{
    public class AlumnoDTO
    {

        [StringLength(13, ErrorMessage = "La identidad debe contener 13 digitos")]
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public Genero Genero { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
