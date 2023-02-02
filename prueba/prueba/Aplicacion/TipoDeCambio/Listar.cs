using MediatR;
using Microsoft.EntityFrameworkCore;
using prueba.Aplicacion.ManejadorError;
using prueba.DTO;
using prueba.Persistencia;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace prueba.Aplicacion.TipoDeCambio
{
    public class Listar
    {
        public class ListaTipoDeCambio : IRequest<List<TipoDeCambioDTO>> { }

        public class Manejador : IRequestHandler<ListaTipoDeCambio, List<TipoDeCambioDTO>>
        {
            private readonly CambioDivisaContext _context;

            public Manejador(CambioDivisaContext context)
            {
                _context = context;
            }

            public async Task<List<TipoDeCambioDTO>> Handle(ListaTipoDeCambio request, CancellationToken cancellationToken)
            {
                var TipoDeCambio = await _context.TipoDeCambio.ToListAsync();

                if (TipoDeCambio == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontraron los tipo de cambio" });
                }

                List<TipoDeCambioDTO> TipoDeCambioDTO = TipoDeCambio.Select(x => new TipoDeCambioDTO { idTipoDeCambio = x.idTipoDeCambio, nombreTipoDeCambio = x.nombreTipoDeCambio, tipoDeCambio = x.tipoDeCambio }).ToList();

                if (TipoDeCambioDTO == null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se pudo realizar la conversión de los tipo de cambio" });
                }

                return TipoDeCambioDTO;
            }
        }
    }
}
