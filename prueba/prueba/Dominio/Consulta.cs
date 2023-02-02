using System;
using System.ComponentModel.DataAnnotations;

namespace prueba.Dominio
{
    public class Consulta
    {
        [Key]
        public int idConsulta { get; set; }
        public double monto { get; set; }
        public int idTipoDeCambio { get; set; }
        public DateTime fechaCreacion { get; set; }
        public TipoDeCambio TipoDeCambio { get; set; }
    }
}
