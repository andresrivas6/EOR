using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Pizzeria.Models
{
    public class Pizza
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [DisplayName("Nombre del Producto")]
        public string NombreProducto { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        [DisplayName("Tamaño")]
        public string Tamano { get; set; }

        [Required]
        [DisplayName("Cantidad de Porciones")]
        public int CantPorciones { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Precio { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public ICollection<Orden> Ordenes { get; set; }
    }
}
