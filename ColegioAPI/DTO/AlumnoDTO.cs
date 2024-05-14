using System.ComponentModel.DataAnnotations;
using ColegioAPI.Models;

namespace ColegioAPI.DTO
{
    public class AlumnoDTO
    {

        [Length(13,13, ErrorMessage = "La identidad debe contener 13 digitos")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Necesita proporcionar el nombre.")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "Necesita proporcionar los apellidos.")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Necesita proporcionar el genero.")]
        public Genero Genero { get; set; }
        [Required(ErrorMessage = "Necesita proporcionar la fecha de nacimiento.")]
        public DateTime FechaNacimiento { get; set; }
    }
}
