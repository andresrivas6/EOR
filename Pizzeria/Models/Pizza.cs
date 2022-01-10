using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
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
        public string NombreProducto { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string Tamano { get; set; }

        [Required]
        public int CantPorciones { get; set; }
        public Decimal Precio { get; set; }
        public string Descripcion { get; set; }

        public ICollection<Orden> Ordenes { get; set; }
    }
}
