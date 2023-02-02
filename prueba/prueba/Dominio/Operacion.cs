using System.ComponentModel.DataAnnotations;

namespace prueba.Dominio
{
    public class Operacion
    {
        [Key]
        public int idOperacion { get; set; }
        public string descripcion { get; set; }
        public TipoDeCambio TipoDeCambio { get; set; }
    }
}
