using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Pizzeria.Models
{
    public class Orden
    {
        [Key]
        [Required]
        [DisplayName("No Orden")]
        public Int64 Id { get; set; }

        [Required]
        [StringLength(300)]
        [DataType(DataType.Text)]
        [DisplayName("Nombre del Solicitante")]
        public string NombreSolicitante { get; set; }

        [Required]
        [DisplayName("Cantidad Ordenada")]
        //[RegularExpression(@"[^0-9]+")]
        [RegularExpression(@"[0-9]+")]
        public int CantOrden { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string Direccion { get; set; }
        [Required]
        public string Comentarios { get; set; }
        public bool Entregada { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public Decimal Total { get; set; }
        [Required]
        public DateTime Fecha { get; set; }
        [DisplayName("Usuario que ingresó")]
        public int UsuarioIngreso { get; set; }
        [ForeignKey("UsuarioIngreso")]
        public Usuario Usuario { get; set; }
        [DisplayName("Tipo de Pizza")]
        public int TipoPizza { get; set; }
        [ForeignKey("TipoPizza")]
        public Pizza Pizza { get; set; }
    }
}
