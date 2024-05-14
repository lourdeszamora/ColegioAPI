using System.ComponentModel.DataAnnotations;

namespace ColegioAPI.DTO
{
    public class AlumnoGradoDTO
    {
        public GradoDTO Grado { get; set; }
        [Required(ErrorMessage = "Necesita proporcionar la seccion.")]
        public int Seccion { get; set; }
    }
}
