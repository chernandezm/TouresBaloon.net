using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ApplicationCore.Entities
{
    public class Canasta
    {
        [Key]
        public int id_canasta { get; set; }
        public int id_producto { get; set; }
        public int id_estado { get; set; }
        public int id_usuario { get; set; }
        public int cantidad_canasta { get; set; }
        public float precio_canasta { get; set; }
    }
}
