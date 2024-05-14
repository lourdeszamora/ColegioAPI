using ColegioAPI.Models;
using System.ComponentModel.DataAnnotations;

namespace ColegioAPI.DTO
{
    public class ProfesorDTO
    {
        [Length(13,13, ErrorMessage = "La identidad debe contener 13 digitos")]
        public string Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Necesita proporcionar el nombre.")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false,ErrorMessage = "Necesita proporcionar los apellidos.")]
        public string Apellidos { get; set; }
        [Required(ErrorMessage = "Necesita proporcionar el genero.")]
        public Genero Genero { get; set; }
    }
}
