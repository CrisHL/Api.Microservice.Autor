namespace Api.Microservice.Autor.Aplicacion
{
    public class AutorDto
    {
        public int AutorId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public DateTime? FechaNacimeinto { get; set; }
        public string AutorLibroGuid { get; set; }

    
    }
}
