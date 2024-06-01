using Api.Microservice.Autor.Modelo;
using Api.Microservice.Autor.Persistencia;
using AutoMapper;
using LinqToDB;
using MediatR;

namespace Api.Microservice.Autor.Aplicacion
{
    public class Consulta
    {
        public class ListAutor : IRequest<List<AutorDto>> 
        {
            
        }

        public class Manejador: IRequestHandler<ListAutor, List<AutorDto>>
        {
            private readonly ContextoAutor _context;
            private readonly IMapper _mapper;

            public Manejador(ContextoAutor context, IMapper mapper)
            {
                _context = context;
                this._mapper = mapper;
            }

            public async Task<List<AutorDto>> Handle (ListAutor request, CancellationToken cancellationToken)
            {
                var autores = await _context.AutorLibros.ToListAsync();
                var autoresDto = _mapper.Map<List<AutorLibro>, List<AutorDto>>(autores);
                return autoresDto;
            }
        }
    }
}
