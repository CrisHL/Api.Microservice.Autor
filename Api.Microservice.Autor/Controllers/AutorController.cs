using Api.Microservice.Autor.Aplicacion;
using Api.Microservice.Autor.Servicios;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Microservice.Autor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ITemporalStorageService _temporalStorageService;

        public AutorController(IMediator mediator, ITemporalStorageService temporalStorageService)
        {
            this._mediator = mediator;
            this._temporalStorageService = temporalStorageService;
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Crear(Nuevo.Ejecuta data)
        {
            await _mediator.Send(data);
            var guid = _temporalStorageService.ObtenerGuid();
            return Ok(new { Unit = Unit.Value, Guid = guid });
        }


        [HttpGet]
        public async Task<ActionResult<List<AutorDto>>> GetAutores()
        {
            return await _mediator.Send(new Consulta.ListAutor());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AutorDto>> GetAutorLibro(string id)
        {
            return await _mediator.Send(new ConsultarFiltro.AutorUnico { AutorGuid = id });
        }

    }
}
