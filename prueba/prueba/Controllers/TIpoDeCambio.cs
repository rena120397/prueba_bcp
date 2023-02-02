using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba.Aplicacion.TipoDeCambio;
using prueba.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace prueba.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class TIpoDeCambio : MyControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<TipoDeCambioDTO>>> Listar()
        {
            return await mediator.Send(new Listar.ListaTipoDeCambio());
        }

        [HttpPost("{id}")]
        public async Task<ActionResult<Unit>> Editar(int id, Actualizar.Ejecuta data)
        {
            data.idTipoDeCambio = id;
            return await mediator.Send(data);
        }
    }
}
