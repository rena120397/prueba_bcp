using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace prueba.Dominio
{
    public class Moneda
    {
        [Key]
        public int idMoneda { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public TipoDeCambio TipoDeCambioOrigen { get; set; }
        public TipoDeCambio TipoDeCambioDestino { get; set; }
    }
}
