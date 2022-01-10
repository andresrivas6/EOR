﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Pizzeria.Models
{
    public class Usuario
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        [DataType(DataType.Text)]
        public string Nombre { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.Text)]
        public string User { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public Byte[] Pass { get; set; }

        public Boolean EsAdmin { get; set; }

        public ICollection<Orden> Ordenes { get; set; }
    }
}
