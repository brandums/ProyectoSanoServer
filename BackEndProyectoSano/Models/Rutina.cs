namespace BackEndProyectoSano.Models
{
    public class Rutina
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Titulo { get; set; }
        public string Subtitulo { get; set; }
        public string Dia { get; set; }
        public int UserId { get; set; }
        public string Image1 { get; set; }
        public string Image2 { get; set; }
        public string[] PosicionInicial { get; set; }
        public string[] Movimientos { get; set; }
        public string[] Respiraciones { get; set; }
        public string[] Repeticiones { get; set; }
    }
}
