using System;
using System.ComponentModel.DataAnnotations;

namespace prueba.Dominio
{
    public class TipoDeCambio
    {
        [Key]
        public int idTipoDeCambio { get; set; }
        public string nombreTipoDeCambio { get; set; }
        public double tipoDeCambio { get; set; }
        public int idMonedaOrigen { get; set; }
        public int idMonedaDestino { get; set; }
        public int idOperacion { get; set; }
        public DateTime fechaCreacion { get; set; }
        public Moneda MonedaOrigen { get; set; }
        public Moneda MonedaDestino { get; set; }
        public Operacion Operacion { get; set; }
        public Transaccion Transaccion { get; set; }
        public Consulta Consulta { get; set; }
    }
}
