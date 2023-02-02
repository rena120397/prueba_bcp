using FluentValidation;
using MediatR;
using prueba.Persistencia;
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
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                //RuleFor(x => x.idTipoDeCambio).NotEmpty().NotNull();
                RuleFor(x => x.tipodeCambio).NotEmpty().NotNull();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta>
        {
            private readonly CambioDivisaContext _context;

            public Manejador(CambioDivisaContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var tipoDeCambio = await _context.TipoDeCambio.FindAsync(request.idTipoDeCambio);
                tipoDeCambio.tipoDeCambio = request.tipodeCambio;

                var resultado = await _context.SaveChangesAsync();
                if (resultado > 0)
                {
                    return Unit.Value;
                }

                return Unit.Value;
            }
        }

    }
}
