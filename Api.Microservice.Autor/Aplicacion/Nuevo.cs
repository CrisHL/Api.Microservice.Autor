using Api.Microservice.Autor.Modelo;
using Api.Microservice.Autor.Persistencia;
using Api.Microservice.Autor.Servicios;
using FluentValidation;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class Nuevo
    {
        public class Ejecuta : IRequest
        {
            public string Nombre { get; set; }
            public string Apellidos { get; set; }
            public DateTime? FechaNacimiento { get; set; }
        }
        //clase para valudar la clase ejecuta a traves del apifluent validator
        public class EjecuteValidation : AbstractValidator<Ejecuta>
        {
            public EjecuteValidation()
            {
                RuleFor(p => p.Nombre).NotEmpty(); //No acepta valores nulos para la propiedad nombre
                RuleFor(p=> p.Apellidos).NotEmpty();
            }
        }

        
        public class Manejador : IRequestHandler<Ejecuta>
        {
            public readonly ContextoAutor _context;
            private readonly ITemporalStorageService _temporalStorageService;
            public Manejador(ContextoAutor context, ITemporalStorageService temporalStorageService)
            {
                _context = context;
                _temporalStorageService = temporalStorageService;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //Se crea la instancia del autor libro ligada al contexto
                var autorLibro = new AutorLibro
                {
                    Nombre = request.Nombre,
                    Apellido = request.Apellidos,
                    FechaNacimiento = request.FechaNacimiento,
                    AutorLibroGuid = Convert.ToString(Guid.NewGuid())
                };
                //agregamos el objeto del tipo autor libro
                _context.AutorLibros.Add(autorLibro);
                //insertamos el valor de inserccion
                var respuesta = await _context.SaveChangesAsync();
                string response;
                if(respuesta > 0)
                {
                    _temporalStorageService.AlmacenarGuid(autorLibro.AutorLibroGuid);
                    return (Unit.Value);
                }
                throw new Exception("No se puedo insertar el Autor del Libro");
            }
        }
    }
}
