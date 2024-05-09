namespace ColegioAPI.Models
{
    public class AlumnoGrado
    {
        public int Id { get; set; }
        public string AlumnoId { get; set; }
        public int GradoId { get; set; }
        public int Seccion { get; set; }

        public Alumno Alumno { get; set; }
        public Grado Grado { get; set; }
    }
}
