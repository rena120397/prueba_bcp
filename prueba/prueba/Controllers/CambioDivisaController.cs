using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using prueba.Aplicacion.ManejadorError;
using prueba.Aplicacion.Proceso;
using prueba.DTO;
using System;
using System.Net;
using System.Threading.Tasks;

namespace prueba.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CambioDivisaController : MyControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<MontoSalidaDTO>> Query(CambioDivisa.Respuesta data)
        {
            return await mediator.Send(data);
        }
    }
}
