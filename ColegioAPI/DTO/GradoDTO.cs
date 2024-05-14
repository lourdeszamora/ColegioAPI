using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace ColegioAPI.DTO
{
    public class GradoDTO
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Necesita proporcionar el nombre.")]
        public string Nombre { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Necesita proporcionar el nivel.")]
        public string ProfesorId { get; set;}
    }
}
