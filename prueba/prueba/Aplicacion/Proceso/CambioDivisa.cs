using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using prueba.Aplicacion.ManejadorError;
using prueba.Dominio;
using prueba.DTO;
using prueba.Persistencia;
using prueba.Utilidades;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace prueba.Aplicacion.Proceso
{
    public class CambioDivisa
    {
        public class Respuesta : IRequest<MontoSalidaDTO>
        {
            public double monto { get; set; }
            public string monedaOrigen { get; set; }
            public string monedaDestino { get; set; }
            public string token { get; set; }
        }

        public class ManejadorValidacion : AbstractValidator<Respuesta>
        {
            public ManejadorValidacion()
            {
                RuleFor(x => x.monto).NotEmpty().NotNull();
                RuleFor(x => x.monedaOrigen).NotEmpty().NotNull();
                RuleFor(x => x.monedaDestino).NotEmpty().NotNull();
                RuleFor(x => x.token).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Respuesta, MontoSalidaDTO>
        {
            private readonly CambioDivisaContext _context;
            private readonly IConfiguration _configuration;

            public Manejador(IConfiguration configuration, CambioDivisaContext context)
            {
                _configuration = configuration;
                _context = context;
            }

            public async Task<MontoSalidaDTO> Handle(Respuesta request, CancellationToken cancellationToken)
            {
                string tokenconfig = _configuration["token"];
                if (request.token== tokenconfig)
                {
                    double montoResultado = 0;

                    var IdMonedaOrigen = await _context.Moneda.Where(x => x.nombre == request.monedaOrigen.ToString()).FirstOrDefaultAsync();

                    if (IdMonedaOrigen == null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró la moneda de origen" });
                    }

                    var IdMonedaDestino = await _context.Moneda.Where(x => x.nombre == request.monedaDestino.ToString()).FirstOrDefaultAsync();

                    if (IdMonedaDestino == null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró la moneda de destino" });
                    }

                    var TipoDeCambio = await _context.TipoDeCambio.Where(x => x.idMonedaOrigen == IdMonedaOrigen.idMoneda && x.idMonedaDestino == IdMonedaDestino.idMoneda).FirstOrDefaultAsync();

                    if (TipoDeCambio == null)
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontró el tipo de cambio" });
                    }

                    if (TipoDeCambio.idOperacion.ToString() == Constantes.CompraMayorPeso)
                    {
                        montoResultado = request.monto * TipoDeCambio.tipoDeCambio;
                    }
                    else
                    {
                        montoResultado = request.monto / TipoDeCambio.tipoDeCambio;
                    }

                    var respuesta = new MontoSalidaDTO();

                    respuesta.monto = request.monto;
                    respuesta.monedaOrigen = request.monedaOrigen;
                    respuesta.monedaDestino = request.monedaDestino;
                    respuesta.tipoDeCambio = TipoDeCambio.tipoDeCambio;
                    respuesta.montoConTipoDeCambio = montoResultado;

                    var consulta = new Consulta
                    {
                        monto = respuesta.monto,
                        idTipoDeCambio = TipoDeCambio.idTipoDeCambio,
                        fechaCreacion = System.DateTime.Now
                    };

                    _context.Consulta.Add(consulta);

                    var valor1 = await _context.SaveChangesAsync();

                    if (valor1 > 0)
                    {
                        return respuesta;
                    }

                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se guardaron los cambios" });
                }
                else
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El token ingresado es invalido" });
                }               
            }
        }
    }
}
