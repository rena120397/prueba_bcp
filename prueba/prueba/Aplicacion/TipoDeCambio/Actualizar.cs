using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using prueba.Aplicacion.ManejadorError;
using prueba.Persistencia;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace prueba.Aplicacion.TipoDeCambio
{
    public class Actualizar
    {
        public class Ejecuta : IRequest
        {
            public int idTipoDeCambio { get; set; }

            public double tipodeCambio { get; set; }

            public string token { get; set; }

            public class EjecutaValidacion : AbstractValidator<Ejecuta>
            {
                public EjecutaValidacion()
                {
                    //RuleFor(x => x.idTipoDeCambio).NotEmpty().NotNull();
                    RuleFor(x => x.tipodeCambio).NotEmpty().NotNull();
                    RuleFor(x => x.token).NotEmpty();
                }
            }

            public class Manejador : IRequestHandler<Ejecuta>
            {
                private readonly CambioDivisaContext _context;
                private readonly IConfiguration _configuration;

                public Manejador(IConfiguration configuration, CambioDivisaContext context)
                {
                    _configuration = configuration;
                    _context = context;
                }

                public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
                {
                    string tokenconfig = _configuration["token"];
                    if (request.token == tokenconfig)
                    {
                        var tipoDeCambio = await _context.TipoDeCambio.FindAsync(request.idTipoDeCambio);

                        if (tipoDeCambio == null)
                        {
                            throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se pudo encontrar el tipo de cambio" });
                        }

                        tipoDeCambio.tipoDeCambio = request.tipodeCambio;

                        var resultado = await _context.SaveChangesAsync();
                        if (resultado > 0)
                        {
                            return Unit.Value;
                        }

                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se pudo actualizar el tipo de cambio" });
                    }
                    else
                    {
                        throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "El token ingresado es invalido" });
                    }
                }
            }
        }
    }
}
