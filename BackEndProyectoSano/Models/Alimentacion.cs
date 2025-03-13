namespace BackEndProyectoSano.Models
{
    public class Alimentacion
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Desayuno { get; set; }
        public string Almuerzo { get; set; }
        public string Cena { get; set; }
        public int UserId { get; set; }
    }
}
