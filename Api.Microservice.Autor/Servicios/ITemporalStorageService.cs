namespace Api.Microservice.Autor.Servicios
{
    public interface ITemporalStorageService
    {
        public void AlmacenarGuid(string guid);
        public string ObtenerGuid();
    }
}
