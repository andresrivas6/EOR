using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pizzeria.Models
{
    public class Orden
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        [DataType(DataType.Text)]
        public string NombreSolicitante { get; set; }

        [Required]
        public int CantOrden { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }
        public string Comentarios { get; set; }
        public bool Entregada { get; set; }
        [Required]
        public double Total { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        public int UsuarioIngreso { get; set; }
        [ForeignKey("UsuarioIngreso")]
        public Usuario Usuario { get; set; }
        public int TipoPizza { get; set; }
        [ForeignKey("TipoPizza")]
        public Pizza Pizza { get; set; }
    }
}
