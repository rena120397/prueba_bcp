using System;
using System.ComponentModel.DataAnnotations;

namespace prueba.Dominio
{
    public class Transaccion
    {
        [Key]
        public int idTransaccion { get; set; }
        public double monto { get; set; }
        public double montoConTipoDeCambio { get; set; }
        public int idTipoDeCambio { get; set; }
        public DateTime fechaCreacion { get; set; }
        public TipoDeCambio TipoDeCambio { get; set; }
    }
}
