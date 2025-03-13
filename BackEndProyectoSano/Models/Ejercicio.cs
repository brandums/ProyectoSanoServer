namespace BackEndProyectoSano.Models
{
    public class Ejercicio
    {
        public string Titulo { get; set; }
        public string Dia { get; set; }
        public List<Rutina> Rutinas { get; set; }
    }
}
