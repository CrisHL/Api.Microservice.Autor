namespace Api.Microservice.Autor.Servicios
{
    public class TemporalStorageService : ITemporalStorageService
    {
        private string _guid;

        public void AlmacenarGuid(string guid)
        {
            _guid = guid;
        }

        public string ObtenerGuid()
        {
            return _guid;
        }
    }
}
